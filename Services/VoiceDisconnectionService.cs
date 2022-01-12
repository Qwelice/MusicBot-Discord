namespace DiscordMusicBot.Services
{
    using DiscordMusicBot.Enums;
    using DiscordMusicBot.Services.Interfaces;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Lavalink;
    using System;
    using System.Threading.Tasks;

    public class VoiceDisconnectionService : IVoiceDisconnectionService
    {
        public async Task DisconnectFromVoice(CommandContext ctx, IServiceProvider services)
        {
            var embed = services.GetService(typeof(IEmbedService)) as IEmbedService;
            var textChannel = ctx.Channel;
            var node = GetLavalinkNode(ctx, services);
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
            var connection = node.GetGuildConnection(voiceChannel.Guild);
            if (connection != null)
            {
                await connection.DisconnectAsync();
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
