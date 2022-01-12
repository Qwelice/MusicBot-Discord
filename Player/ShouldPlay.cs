namespace DiscordMusicBot.Player
{
    using DiscordMusicBot.Player.Enums;

    public class ShouldPlay
    {
        public Reason Reason { get; private set; } = Reason.None;
        public bool Result => Reason == Reason.None;
        public void Reset()
        {
            Reason = Reason.None;
        }

        public void SetReason(Reason reason)
        {
            Reason = reason;
        }
    }
}
