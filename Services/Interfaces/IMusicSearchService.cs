namespace DiscordMusicBot.Services.Interfaces
{
    using DSharpPlus.Lavalink;
    public interface IMusicSearchService
    {
        public Task<LavalinkLoadResult?> GetQueryResult(LavalinkGuildConnection conn, string query);
    }
}
