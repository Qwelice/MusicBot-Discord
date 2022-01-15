namespace DiscordMusicBot.Player.Payloads
{
    using DiscordMusicBot.Player.Payloads.Enums;
    using DSharpPlus.CommandsNext;

    public class PausePayload : Payload
    {
        public PausePayload(CommandContext ctx) : base(ctx, PayloadType.Pause)
        {
        }
    }
}
