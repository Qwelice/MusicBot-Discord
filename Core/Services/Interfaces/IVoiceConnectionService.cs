namespace DiscordMusicBot.Services.Interfaces
{
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Lavalink;
    public interface IVoiceConnectionService
    {
        public bool IsExist(CommandContext ctx);
        public LavalinkGuildConnection? GetConnection(CommandContext ctx);
        public Task<LavalinkGuildConnection?> ConnectToVoice(CommandContext ctx, IServiceProvider services);
        public Task DisconnectFromVoice(CommandContext ctx, IServiceProvider services);
    }
}
