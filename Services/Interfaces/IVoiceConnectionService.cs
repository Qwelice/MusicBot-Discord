namespace DiscordMusicBot.Services.Interfaces
{
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Lavalink;
    public interface IVoiceConnectionService
    {
        public Task<LavalinkGuildConnection?> ConnectToVoice(CommandContext ctx, IServiceProvider services);
    }
}
