namespace DiscordMusicBot.Player.Payloads
{
    using DiscordMusicBot.Player.Payloads.Enums;
    using DSharpPlus.CommandsNext;

    public class PlayPayload : Payload
    {
        public PlayPayload(CommandContext ctx) : base(ctx, PayloadType.Play)
        {
        }
    }
}
