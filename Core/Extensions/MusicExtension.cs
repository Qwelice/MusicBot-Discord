namespace DiscordMusicBot.Extensions
{
    using DiscordMusicBot.Extensions.Interfaces;
    using DSharpPlus;
    using DSharpPlus.Lavalink;
    using DSharpPlus.Net;
    using System.Threading.Tasks;

    public class MusicExtension : IExtension
    {
        public Task Setup(DiscordClient client)
        {
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1",
                Port = 2333
            };
            var lava = client.UseLavalink();
            client.SocketOpened += async (s, e) => await ConnectLavalink(lava, endpoint);

            return Task.CompletedTask;
        }

        private async Task ConnectLavalink(LavalinkExtension lava, ConnectionEndpoint endp)
        {
            var config = new LavalinkConfiguration
            {
                Password = "youshallnotpass",
                RestEndpoint = endp,
                SocketEndpoint = endp
            };

            await lava.ConnectAsync(config); // сервер по-любому должен быть запущен, иначе бот вообще будет недоступен!! 
        }
    }
}
