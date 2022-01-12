namespace DiscordMusicBot.Services
{
    using DiscordMusicBot.Services.Interfaces;
    using DSharpPlus.Lavalink;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;

    public class MusicSearchService : IMusicSearchService
    {
        public async Task<IList<LavalinkTrack>?> GetQueryResult(LavalinkGuildConnection conn, string query)
        {
            var isUri = Uri.IsWellFormedUriString(query, UriKind.Absolute);
            IList<LavalinkTrack> loadResult;
            if (isUri)
            {
                var uri = new Uri(query);
                var lResult = await conn.GetTracksAsync(uri);

                if (lResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || lResult.LoadResultType == LavalinkLoadResultType.NoMatches)
                {
                    return null;
                }

                loadResult = lResult.Tracks.ToList();
            }
            else
            {
                var lResult = await conn.GetTracksAsync(query);

                if (lResult.LoadResultType == LavalinkLoadResultType.LoadFailed
                || lResult.LoadResultType == LavalinkLoadResultType.NoMatches)
                {
                    return null;
                }

                loadResult = lResult.Tracks.Take(1).ToList();
            }
            return loadResult;
        }
    }
}
