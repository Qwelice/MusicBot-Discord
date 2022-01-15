namespace DiscordMusicBot.Installation
{
    using DiscordMusicBot.Handlers;
    using DSharpPlus.CommandsNext;
    using System;

    /// <summary>
    /// Установщик обработчиков комманд
    /// </summary>
    public class HandlersInstaller : Installer
    {
        private IServiceProvider _services;
        public void HandleCommands(CommandsNextExtension commands)
        {
            _services = commands.Services;
            // Список типов, являющихся обработчиками команд
            // (они обязательно должны лежать в пространствах, имена которых заканчиваются на Handlers)
            var extList = _assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Handlers") && t.IsClass && t.Name.EndsWith("CommandHandler"))
                .ToList();
            // Инициализация обработчиков команд
            foreach (var extType in extList)
            {
                var ext = Activator.CreateInstance(extType, _services) as BaseHandler;
                if(ext != null)
                {
                    commands.CommandExecuted += ext.HandleCommands;
                }
            }
        }
    }
}
