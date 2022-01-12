namespace DiscordMusicBot.Commands
{
    using DiscordMusicBot.Attributes;
    using DiscordMusicBot.Commands.User.Enums;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    public class UserCommands : BaseCommandModule
    {
        [Command("join"), Aliases("j")]
        [UserCommand(CommandType.Join)]
        public async Task JoinCommand(CommandContext context) { }

        [Command("leave"), Aliases("l")]
        [UserCommand(CommandType.Leave)]
        public async Task LeaveCommand(CommandContext ctx) { }

        [Command("play"), Aliases("p")]
        [UserCommand(CommandType.Play)]
        public async Task PlayCommand(CommandContext ctx, [RemainingText, Query] string query) { }

        [Command("pause"), Aliases("ps")]
        [UserCommand(CommandType.Pause)]
        public async Task PauseCommand(CommandContext ctx) { }

        [Command("stop"), Aliases("s")]
        [UserCommand(CommandType.Stop)]
        public async Task StopCommand(CommandContext ctx) { }

        [Command("next"), Aliases("n")]
        [UserCommand(CommandType.Next)]
        public async Task NextCommand(CommandContext ctx) { }

        [Command("back"), Aliases("b")]
        [UserCommand(CommandType.Back)]
        public async Task BackCommand(CommandContext ctx) { }

        //[Command("help"), Aliases("h")]
        //[UserCommand(CommandType.Help)]
        //public async Task HelpCommand(CommandContext ctx) { }

        //[Command("uct4as")]
        //[UserCommand(CommandType.None)]
        //public async Task NoneCommand(CommandContext ctx) { }
    }
}
