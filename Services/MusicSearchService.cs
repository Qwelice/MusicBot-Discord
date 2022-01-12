namespace DiscordMusicBot.Services
{
    using DiscordMusicBot.Services.Interfaces;
    using DSharpPlus.Lavalink;
    using System;
    using System.Threading.Tasks;

    public class MusicSearchService : IMusicSearchService
    {
        public async Task<LavalinkLoadResult?> GetQueryResult(LavalinkGuildConnection conn, string query)
        {
            var isUri = Uri.IsWellFormedUriString(query, UriKind.Absolute);
            LavalinkLoadResult loadResult;
            if (isUri)
            {
                var uri = new Uri(query);
                loadResult = await conn.GetTracksAsync(uri);
            }
            else
            {
                loadResult = await conn.GetTracksAsync(query);
            }

            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                return null;
            }

            return loadResult;
        }
    }
}
