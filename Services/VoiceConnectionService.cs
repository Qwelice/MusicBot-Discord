namespace DiscordMusicBot.Services
{
    using DiscordMusicBot.Enums;
    using DiscordMusicBot.Services.Interfaces;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Lavalink;
    using System;
    using System.Threading.Tasks;

    public class VoiceConnectionService : IVoiceConnectionService
    {

        private Dictionary<ulong, LavalinkGuildConnection> _connections = new Dictionary<ulong, LavalinkGuildConnection>();

        public bool IsExist(CommandContext ctx)
        {
            var guildId = ctx.Guild.Id;
            return _connections.ContainsKey(guildId);
        }

        public LavalinkGuildConnection? GetConnection(CommandContext ctx)
        {
            if (IsExist(ctx))
            {
                return _connections[ctx.Guild.Id];
            }
            else
            {
                return null;
            }
        }

        public async Task<LavalinkGuildConnection?> ConnectToVoice(CommandContext ctx, IServiceProvider services)
        {
            LavalinkGuildConnection connection;

            var embed = services.GetService(typeof(IEmbedService)) as IEmbedService;
            var textChannel = ctx.Channel;
            var node = GetLavalinkNode(ctx, services);

            var guildId = ctx.Guild.Id;

            if (node == null)
            {
                await textChannel.SendMessageAsync(
                    embed!.CreateEmbed(EmbedType.NoServerConnection)
                    );
                return null;
            }

            var voiceChannel = ctx.Member.VoiceState?.Channel;

            if (voiceChannel == null)
            {
                await textChannel.SendMessageAsync(
                    embed!.CreateEmbed(EmbedType.NotInVoiceChannel)
                    );
                return null;
            }

            if (_connections.ContainsKey(guildId))
            {
                connection = _connections[guildId];
                if(!connection.IsConnected)
                {
                    connection = await node.ConnectAsync(voiceChannel);
                    _connections[guildId] = connection;
                }
            }
            else
            {
                connection = node.GetGuildConnection(ctx.Guild);

                if (connection == null)
                {
                    connection = await node.ConnectAsync(voiceChannel);
                }
                _connections[guildId] = connection;
            }
            return connection;
        }

        public async Task DisconnectFromVoice(CommandContext ctx, IServiceProvider services)
        {
            var embed = services.GetService(typeof(IEmbedService)) as IEmbedService;
            var textChannel = ctx.Channel;
            var node = GetLavalinkNode(ctx, services);
            var guildId = ctx.Guild.Id;

            if (node == null)
            {
                await textChannel.SendMessageAsync(
                    embed!.CreateEmbed(EmbedType.NoServerConnection)
                    );
                return;
            }
            var voiceChannel = ctx.Member.VoiceState?.Channel;
            if (voiceChannel == null)
            {
                await textChannel.SendMessageAsync(
                    embed!.CreateEmbed(EmbedType.NotInVoiceChannel)
                    );
                return;
            }

            if (_connections.ContainsKey(guildId))
            {
                var connection = _connections[guildId];
                if (connection.IsConnected)
                {
                    await connection.DisconnectAsync();
                }
                _connections.Remove(guildId);
            }
        }

        private LavalinkNodeConnection? GetLavalinkNode(CommandContext ctx, IServiceProvider services)
        {
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values?.First();
            return node;
        }
    }
}
