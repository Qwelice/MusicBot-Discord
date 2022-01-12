namespace DiscordMusicBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BotStarter.Start().GetAwaiter().GetResult();
        }
    }
}
