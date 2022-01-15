namespace DiscordMusicBot.Player.Payloads
{
    using DiscordMusicBot.Player.Payloads.Enums;
    using DSharpPlus.CommandsNext;

    public class QueryPayload : Payload
    {
        public string Query { get; private set; }
        public QueryPayload(CommandContext ctx, string query) : base(ctx, PayloadType.Query)
        {
            Query = query;
        }
    }
}
