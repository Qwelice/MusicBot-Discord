namespace DiscordMusicBot
{
    using DiscordMusicBot.Configurations;
    using DiscordMusicBot.Configurations.Models;
    using DiscordMusicBot.DB.Init;
    using DiscordMusicBot.Installation;
    using DSharpPlus;
    using DSharpPlus.CommandsNext;
    using System;
    
    public class BotStarter
    {
        public static async Task Start()
        {
            BotConfiguration botCfg = Configurator.GetConfiguration<BotConfiguration>();
            DiscordClient client = new DiscordClient(new DiscordConfiguration()
            {
                Token = botCfg.Token,
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

            DefaultInitializer.Seed(false, client);

            await Task.Delay(-1);
        }
    }
}
