namespace DiscordMusicBot.Player.Payloads
{
    using DiscordMusicBot.Player.Payloads.Enums;
    using DSharpPlus.CommandsNext;

    public abstract class Payload
    {
        public PayloadType Type { get; private set; } = PayloadType.None;
        public CommandContext Context { get; private set; }

        public Payload(CommandContext ctx, PayloadType type)
        {
            Context = ctx;
            Type = type;
        }
    }
}
