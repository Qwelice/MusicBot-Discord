namespace DiscordMusicBot.Commands
{
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Converters;
    using DSharpPlus.CommandsNext.Entities;
    using DSharpPlus.Entities;

    public class BotHelpFormatter : DefaultHelpFormatter
    {
        public BotHelpFormatter(CommandContext ctx) : base(ctx) { }

        public override CommandHelpMessage Build()
        {
            EmbedBuilder.Title = "Как мною пользоваться";
            EmbedBuilder.Description = "Привет, это небольшое описание того, что я умею делать\n" +
                "Для любой команды я воспринимаю следующие префиксы: \"-\", \"!\"\n" +
                "Со мной (пока что) можно общаться исключительно на уровне команд\n";
            EmbedBuilder.Author = new DiscordEmbedBuilder.EmbedAuthor();
            EmbedBuilder
                .WithColor(DiscordColor.Lilac)
                .AddField("\'help\' (или \'h\')", "Делюсь с вами своими возможностями")
                .AddField("\'play\' (или \'p\')", "Без параметров (если после команды ничего не введено): запускаю приостановленную музыку\n" +
                                                  "С параметром (если после команды что-то введено): запускаю музыку, название (или ссылка) которой была введена")
                .AddField("\'pause\' (или \'ps\')", "Приостанавливаю музыку, которая играет в данный момент времени")
                .AddField("\'stop\' (или \'s\')", "Полностью останавливаю воспроизведение музыки, а также очищаю очередь")
                .AddField("\'next\' (или \'n\')", "Запускаю следующую по очереди музыку")
                .AddField("\'back\' (или \'b\')", "Запускаю предыдущую по очереди музыку")
                .AddField("\'leave\' (или \'l\')", "Покидаю голосовой канал");
                //.AddField("\'offer\'", "С параметром (если после команды что-то введено): то, что было вами введено отправляю в свою \"Книгу Жалоб и Предложений\"");
            return base.Build();
        }
    }
}
