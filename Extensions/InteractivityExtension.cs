namespace DiscordMusicBot.Extensions
{
    using DiscordMusicBot.Extensions.Interfaces;
    using DSharpPlus;
    using DSharpPlus.Interactivity;
    using DSharpPlus.Interactivity.Enums;
    using DSharpPlus.Interactivity.Extensions;

    public class InteractivityExtension : IExtension
    {
        public Task Setup(DiscordClient client)
        {
            client.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(45)
            });
            return Task.CompletedTask;
        }
    }
}
