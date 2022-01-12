namespace DiscordMusicBot.Services.Interfaces
{
    using DSharpPlus.Lavalink;
    public interface IMusicSearchService
    {
        public Task<IList<LavalinkTrack>?> GetQueryResult(LavalinkGuildConnection conn, string query);
    }
}
