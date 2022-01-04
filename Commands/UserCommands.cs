namespace DiscordMusicBot.Commands
{
    using DiscordMusicBot.Attributes;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    public class UserCommands : BaseCommandModule
    {
        [Command("join"), Aliases("j")]
        [UserCommand(Enums.CommandType.Join)]
        public async Task JoinCommand(CommandContext context) { }

        [Command("leave"), Aliases("l")]
        [UserCommand(Enums.CommandType.Leave)]
        public async Task LeaveCommand(CommandContext ctx) { }

        [Command("play"), Aliases("p")]
        [UserCommand(Enums.CommandType.Play)]
        public async Task PlayCommand(CommandContext ctx, [RemainingText, Query] string query) { }

        [Command("pause"), Aliases("ps")]
        [UserCommand(Enums.CommandType.Pause)]
        public async Task PauseCommand(CommandContext ctx) { }

        [Command("stop"), Aliases("s")]
        [UserCommand(Enums.CommandType.Stop)]
        public async Task StopCommand(CommandContext ctx) { }

        [Command("next"), Aliases("n")]
        [UserCommand(Enums.CommandType.Next)]
        public async Task NextCommand(CommandContext ctx) { }

        [Command("back"), Aliases("b")]
        [UserCommand(Enums.CommandType.Back)]
        public async Task BackCommand(CommandContext ctx) { }
    }
}
