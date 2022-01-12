namespace DiscordMusicBot.Handlers
{
    using DiscordMusicBot.Attributes;
    using DiscordMusicBot.Commands.User.Enums;
    using DSharpPlus.CommandsNext;
    using System;
    using System.Threading.Tasks;

    public class UserCommandHandler : BaseHandler
    {
        public UserCommandHandler(IServiceProvider services) : base(services) { }

        public override async Task HandleCommands(CommandsNextExtension commands, CommandExecutionEventArgs args)
        {
            var cmd = args.Command;
            if (!IsUserCommand(cmd))
            {
                return;
            }
            var type = GetUserCommandType(cmd);
            if(type == CommandType.Join)
            {
                
            }
            else if(type == CommandType.Leave)
            {

            }
            else if(type == CommandType.Play)
            {

            }
            else if(type == CommandType.Pause)
            {

            }
            else if(type == CommandType.Stop)
            {

            }
            else if (type == CommandType.Next)
            {

            }
            else if (type == CommandType.Back)
            {

            }
            else if (type == CommandType.Help)
            {

            }
            else if(type == CommandType.None)
            {

            }
        }

        private CommandType GetUserCommandType(Command cmd)
        {
            var attr = cmd.CustomAttributes
                .Where(a => a is UserCommandAttribute)
                .First() as UserCommandAttribute;
            return attr!.Type;
        }

        private bool IsUserCommand(Command cmd)
        {
            return cmd.CustomAttributes.Where(a => a is UserCommandAttribute).Any();
        }
    }
}
