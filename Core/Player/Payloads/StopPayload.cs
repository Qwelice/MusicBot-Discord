namespace DiscordMusicBot.Player.Payloads
{
    using DiscordMusicBot.Player.Payloads.Enums;
    using DSharpPlus.CommandsNext;

    public class StopPayload : Payload
    {
        public StopPayload(CommandContext ctx) : base(ctx, PayloadType.Stop)
        {
        }
    }
}
