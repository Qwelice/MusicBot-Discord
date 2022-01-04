namespace DiscordMusicBot.Handlers
{
    using DiscordMusicBot.Attributes;
    using DiscordMusicBot.Enums;
    using DSharpPlus.CommandsNext;
    using System;
    using System.Threading.Tasks;

    public class UserCommandHandler : BaseHandler
    {
        public UserCommandHandler(IServiceProvider services) : base(services) { }

        public override Task HandleCommands(CommandsNextExtension commands, CommandExecutionEventArgs args)
        {
            throw new NotImplementedException();
        }

        private CommandType GetUserCommandType(Command cmd)
        {
            var attr = cmd.CustomAttributes
                .Where(a => a is UserCommandAttribute)
                .First() as UserCommandAttribute;
            return attr.Type;
        }

        private bool IsUserCommand(Command cmd)
        {
            return cmd.CustomAttributes.Where(a => a is UserCommandAttribute).Any();
        }
    }
}
