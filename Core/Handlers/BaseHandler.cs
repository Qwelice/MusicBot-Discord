namespace DiscordMusicBot.Handlers
{
    using DSharpPlus.CommandsNext;

    public abstract class BaseHandler
    {
        protected IServiceProvider _services;
        public BaseHandler(IServiceProvider services)
        {
            _services = services;
        }
        public abstract Task HandleCommands(CommandsNextExtension commands, CommandExecutionEventArgs args);
    }
}
