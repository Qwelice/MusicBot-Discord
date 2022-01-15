namespace DiscordMusicBot.Player.Payloads
{
    using DiscordMusicBot.Player.Payloads.Enums;
    using DSharpPlus.CommandsNext;

    public class NextPayload : Payload
    {
        public NextPayload(CommandContext ctx) : base(ctx, PayloadType.Next)
        {
        }
    }
}
