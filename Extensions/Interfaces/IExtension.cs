namespace DiscordMusicBot.Extensions.Interfaces
{
    using DSharpPlus;
    using System.Threading.Tasks;

    public interface IExtension
    {
        public Task Setup(DiscordClient client);
    }
}
