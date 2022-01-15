namespace DiscordMusicBot.Installation
{
    using DiscordMusicBot.Commands;
    using DSharpPlus;
    using DSharpPlus.CommandsNext;
    using System;

    public class CommandsInstaller : Installer
    {
        private string[] _prefixes = new[] { "!", "-" };

        public CommandsNextExtension SetupAndGetCommands(DiscordClient client, IServiceProvider services)
        {
            CommandsNextExtension commands = null;

            EnableAndConfigureCommands(client, ref commands, services);
            RegisterCommands(commands);

            return commands;
        }

        public void SetupCommands(DiscordClient client, ref CommandsNextExtension commands, IServiceProvider services)
        {
            EnableAndConfigureCommands(client, ref commands, services);
            RegisterCommands(commands);
        }

        private void EnableAndConfigureCommands(DiscordClient client, ref CommandsNextExtension commands, IServiceProvider services)
        {
            commands = client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = _prefixes,
                Services = services
            });
            commands.SetHelpFormatter<BotHelpFormatter>();
        }

        private void RegisterCommands(CommandsNextExtension commands)
        {
            var commandsList = _assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Commands") 
                          && !t.Name.StartsWith("<") && t.Name.EndsWith("Commands"))
                .ToList();
            foreach (var c in commandsList)
            {
                commands.RegisterCommands(c);
            }
        }
    }
}
