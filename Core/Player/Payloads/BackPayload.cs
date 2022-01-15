namespace DiscordMusicBot.Player.Payloads
{
    using DiscordMusicBot.Player.Payloads.Enums;
    using DSharpPlus.CommandsNext;
    public class BackPayload : Payload
    {
        public BackPayload(CommandContext ctx) : base(ctx, PayloadType.Back)
        {
        }
    }
}
