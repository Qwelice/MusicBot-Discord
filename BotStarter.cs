namespace DiscordMusicBot
{
    using DiscordMusicBot.Installation;
    using DSharpPlus;
    using DSharpPlus.CommandsNext;
    using System;
    
    public class BotStarter
    {
        public static async Task Start()
        {
            DiscordClient client = new DiscordClient(new DiscordConfiguration()
            {
                Token = "ODkzNjE5MTY1NjgyODkyODUy.YVeFsw.MOZ-iOjJ8jHYgnjoKZi_RA6LA2M",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged,
                AutoReconnect = true
            });

            IServiceProvider services = new ServicesInstaller()
                .SetupAndGetServices();
            CommandsNextExtension commands = new CommandsInstaller()
                .SetupAndGetCommands(client, services);
            new HandlersInstaller()
                .HandleCommands(commands);
            new ExtensionsInstaller()
                .SetupExtensions(client);

            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
