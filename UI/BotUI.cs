namespace DiscordMusicBot.UI
{
    using DiscordMusicBot.UI.Buttons;
    using DSharpPlus.Entities;
    using System;

    public static class BotUI
    {
        public static DiscordMessageBuilder GetBotMenu()
        {
            var builder = new DiscordMessageBuilder();
            builder.AddComponents(
                BotButtons.GetStatisticsButton()
                );
            builder.WithContent("Меню");
            return builder;
        }
    }
}
