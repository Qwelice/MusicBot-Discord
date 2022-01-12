namespace DiscordMusicBot.Player
{
    using DiscordMusicBot.Enums;
    using DiscordMusicBot.Player.Enums;
    using DiscordMusicBot.Player.Payloads;
    using DiscordMusicBot.Player.Payloads.Enums;
    using DiscordMusicBot.Services.Interfaces;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Entities;
    using DSharpPlus.Lavalink;
    using System;

    public class MusicPlayer
    {
        private delegate Task PayloadAction();
        private delegate Task TrackListAction(CommandContext ctx, LavalinkGuildConnection connection);
        private event PayloadAction OnPayloadAppend;
        private event TrackListAction OnTrackListUpdated;

        private IServiceProvider _services;
        private TrackList _tracks = new TrackList();
        private Queue<Payload> _payloads = new Queue<Payload>();

        private ShouldPlay _shouldPlay = new ShouldPlay();

        private DiscordMessage _msg;
        public PlayerState State { get; private set; } = PlayerState.None;

        public MusicPlayer(IServiceProvider services)
        {
            _services = services;
            OnPayloadAppend += ExecutePayloads;
            OnTrackListUpdated += PlayTrack;
        }

        public async Task AppendPayload(Payload payload)
        {
            lock (_payloads)
            {
                _payloads.Enqueue(payload);
            }
            await OnPayloadAppend?.Invoke();
        }

        private async Task ExecutePayloads()
        {
            while (_payloads.Count > 0)
            {
                Payload payload;
                lock (_payloads)
                {
                    payload = _payloads.Dequeue();
                }
                var connService = _services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService;
                var conn = connService!.GetConnection(payload.Context);
                if (conn == null)
                {
                    conn = await connService.ConnectToVoice(payload.Context, _services);
                    HandleConnectionEvents(payload.Context, conn!);
                }
                if (payload.Type == PayloadType.Query)
                {
                    await HandleQueryPayload(payload as QueryPayload);
                }
                else if (payload.Type == PayloadType.Play)
                {
                    await HandlePlayPayload(payload as PlayPayload);
                }
                else if (payload.Type == PayloadType.Pause)
                {
                    await HandlePausePayload(payload as PausePayload);
                }
                else if (payload.Type == PayloadType.Stop)
                {
                    await HandleStopPayload(payload as StopPayload);
                }
                else if (payload.Type == PayloadType.Next)
                {
                    await HandleNextPayload(payload as NextPayload);
                }
                else if (payload.Type == PayloadType.Back)
                {
                    await HandleBackPayload(payload as BackPayload);
                }
            }
        }

        private void HandleConnectionEvents(CommandContext ctx, LavalinkGuildConnection connection)
        {
            connection.PlaybackStarted += async (s, e) =>
            {
                State = PlayerState.Playing;
                _msg = await ctx.Channel.SendMessageAsync(
                (_services.GetService(typeof(IEmbedService)) as IEmbedService)!
                .CreateNowPlayingEmbed(_tracks.CurrentTrack!)
                );
            };
            connection.PlaybackFinished += async (k, t) =>
            {
                State = PlayerState.None;
                await _msg.DeleteAsync();
                if (_shouldPlay.Result)
                {
                    LavalinkTrack? track;
                    lock (_tracks)
                    {
                        track = _tracks.GetNextTrack();
                    }
                    if (track == null)
                    {
                        _tracks.Clear();
                        _shouldPlay.Reset();
                        return;
                    }
                    else
                    {
                        await PlayTrack(track, connection);
                    }
                }
                _shouldPlay.Reset();
            };
        }

        private async Task PlayTrack(LavalinkTrack track, LavalinkGuildConnection connection)
        {
            if(State == PlayerState.None || State == PlayerState.Stopped)
            {
                await connection.PlayAsync(track);
                State = PlayerState.Playing;
            }
        }
        private async Task PlayTrack(CommandContext ctx, LavalinkGuildConnection connection)
        {
            if (State == PlayerState.None || State == PlayerState.Stopped)
            {
                await connection.PlayAsync(_tracks.CurrentTrack);
                State = PlayerState.Playing;
            }
        }

        private async Task HandleQueryPayload(QueryPayload payload)
        {
            var embed = (_services.GetService(typeof(IEmbedService)) as IEmbedService)!;
            var connection = (_services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService)!
                .GetConnection(payload.Context)!;
            var loadResult = await (_services.GetService(typeof(IMusicSearchService)) as IMusicSearchService)!
                .GetQueryResult(connection, payload.Query);
            if (loadResult == null)
            {
                await payload.Context.Channel.SendMessageAsync(
                    embed.CreateNoQueryResultEmbed(payload.Query)
                    );
                return;
            }
            lock (_tracks)
            {
                _tracks.AddTracks(loadResult);
            }
            await payload.Context.Channel.SendMessageAsync(
                embed.CreateAddedInQueueEmbed(payload.Context, loadResult)
                );
            await OnTrackListUpdated?.Invoke(payload.Context, connection);
        }

        private async Task HandlePlayPayload(PlayPayload payload)
        {
            var embed = _services.GetService(typeof(IEmbedService)) as IEmbedService;
            if (State == PlayerState.Playing)
            {
                return;
            }
            if (State == PlayerState.Stopped || State == PlayerState.None)
            {
                await payload.Context.Channel.SendMessageAsync(
                    embed.CreateEmbed(EmbedType.EmptyQueue)
                    );
                return;
            }
            var conn = (_services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService)!
                .GetConnection(payload.Context)!;
            await conn.ResumeAsync();
        }

        private async Task HandlePausePayload(PausePayload payload)
        {
            if (State == PlayerState.Paused || State == PlayerState.Stopped || State == PlayerState.None)
            {
                return;
            }
            else
            {
                var conn = (_services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService)!
                .GetConnection(payload.Context)!;
                await conn.PauseAsync();
                State = PlayerState.Paused;
            }
        }

        private async Task HandleNextPayload(NextPayload payload)
        {
            if (State == PlayerState.Stopped)
            {
                return;
            }
            else
            {
                LavalinkTrack track;
                lock (_tracks)
                {
                    track = _tracks.GetNextTrack();
                }
                if (track == null)
                {
                    return;
                }
                _shouldPlay.SetReason(Reason.NextTrack);
                var conn = (_services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService)!
                .GetConnection(payload.Context)!;
                await conn.PlayAsync(track);
                State = PlayerState.Playing;
            }
        }

        private async Task HandleBackPayload(BackPayload payload)
        {
            if (State == PlayerState.Stopped)
            {
                return;
            }
            else
            {
                LavalinkTrack track;
                lock (_tracks)
                {
                    track = _tracks.GetPreviousTrack();
                }
                if (track == null)
                {
                    return;
                }
                _shouldPlay.SetReason(Reason.PreviosTrack);
                var conn = (_services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService)!
                .GetConnection(payload.Context)!;
                await conn.PlayAsync(track);
                State = PlayerState.Playing;
            }
        }

        private async Task HandleStopPayload(StopPayload payload)
        {
            if (State == PlayerState.Stopped)
            {
                return;
            }
            else
            {
                var conn = (_services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService)!
                .GetConnection(payload.Context)!;
                await conn.StopAsync();
                State = PlayerState.Stopped;
                _tracks.Clear();
            }
        }
    }
}
