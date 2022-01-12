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
        public async Task<LavalinkGuildConnection?> ConnectToVoice(CommandContext ctx, IServiceProvider services)
        {
            var embed = services.GetService(typeof(IEmbedService)) as IEmbedService;
            var textChannel = ctx.Channel;

            var node = GetLavalinkNode(ctx, services);

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

            var connection = node.GetGuildConnection(ctx.Guild);

            if (connection == null)
            {
                connection = await node.ConnectAsync(voiceChannel);
            }
            return connection;
        }

        private LavalinkNodeConnection? GetLavalinkNode(CommandContext ctx, IServiceProvider services)
        {
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values?.First();
            return node;
        }
    }
}
