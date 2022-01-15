namespace DiscordMusicBot.UI.Buttons
{
    using DSharpPlus;
    using DSharpPlus.Entities;

    public static class BotButtons
    {
        public static DiscordButtonComponent GetStatisticsButton()
        {
            var button = new DiscordButtonComponent(
                ButtonStyle.Primary, "statistics", "Статистика"
                );
            return button;
        }
    }
}
