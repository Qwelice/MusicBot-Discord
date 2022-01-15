namespace DiscordMusicBot.Services
{
    using DiscordMusicBot.Enums;
    using DiscordMusicBot.Services.Interfaces;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.Entities;
    using DSharpPlus.Lavalink;
    using System;
    using System.Text;

    public class EmbedService : IEmbedService
    {
        public DiscordEmbed CreateAddedInQueueEmbed(CommandContext ctx, IList<LavalinkTrack> tracks)
        {
            var emb = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Green)
                .WithTitle("Добавлено в очередь")
                .WithFooter($"Автор запроса {ctx.Member.DisplayName}", ctx.Member.AvatarUrl)
                .WithTimestamp(DateTime.UtcNow);

            var sb = new StringBuilder();
            foreach (var track in tracks)
            {
                sb.Append(track.Title).Append("  -  ").Append(track.Author).Append("\n");
            }
            emb.WithDescription(sb.ToString());
            return emb.Build();
        }

        public DiscordEmbed CreateEmbed(EmbedType type)
        {
            DiscordEmbed embed = null;

            if (type == EmbedType.EmptyQueue)
            {
                embed = new DiscordEmbedBuilder()
                    .WithAuthor("Нечего запускать")
                    .WithDescription("В очереди нет треков для воспроизведения")
                    .WithColor(DiscordColor.DarkRed)
                    .Build();
            }
            else if (type == EmbedType.TrackIsntChosen)
            {
                embed = new DiscordEmbedBuilder()
                    .WithAuthor("Трек не был выбран")
                    .WithDescription("Ничего запускать не буду")
                    .WithColor(DiscordColor.DarkBlue)
                    .Build();
            }
            else if (type == EmbedType.NothingToPlay)
            {
                embed = new DiscordEmbedBuilder()
                    .WithAuthor("Нечего воспроизводить")
                    .WithDescription("Нет в очереди нет треков")
                    .WithColor(DiscordColor.DarkRed)
                    .Build();
            }
            else if (type == EmbedType.NothingToPause)
            {
                embed = new DiscordEmbedBuilder()
                    .WithAuthor("Нечего паузить")
                    .WithDescription("Чтобы что-то запаузить, нужно, чтобы что-то воспроизводилось")
                    .WithColor(DiscordColor.DarkRed)
                    .Build();
            }
            else if (type == EmbedType.NothingToStop)
            {
                embed = new DiscordEmbedBuilder()
                    .WithAuthor("Нечего останавливать")
                    .WithDescription("Чтобы что-то остановить, нужно, чтобы что-то воспроизводилось")
                    .WithColor(DiscordColor.DarkRed)
                    .Build();
            }
            else if (type == EmbedType.NoServerConnection)
            {
                embed = new DiscordEmbedBuilder()
                    .WithAuthor("Нет соединения с сервером")
                    .WithDescription("Кажется, бот не нашел общего языка с сервером")
                    .WithColor(DiscordColor.DarkRed)
                    .Build();
            }
            else if (type == EmbedType.NotInVoiceChannel)
            {
                embed = new DiscordEmbedBuilder()
                    .WithAuthor("Зайдите в голосовой канал")
                    .WithDescription("Прежде чем давать команды для голосового канала, нужно быть в голосовом канале")
                    .WithColor(DiscordColor.DarkRed)
                    .Build();
            }

            return embed;
        }

        public DiscordEmbed CreateNoQueryResultEmbed(string query)
        {
            var embed = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.DarkRed)
                .WithTitle("Не удалось найти")
                .WithDescription($"Нет результатов по запросу: {query}")
                .Build();
            return embed;
        }

        public DiscordEmbed CreateNowPlayingEmbed(LavalinkTrack track)
        {
            var embed = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("Сейчас играет")
                .WithDescription(track.Title)
                .WithFooter(track.Author)
                .Build();
            return embed;
        }
    }
}
