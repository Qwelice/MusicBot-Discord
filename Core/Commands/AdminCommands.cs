namespace DiscordMusicBot.Core.Commands
{
    using DiscordMusicBot.Commands.Enums;
    using DiscordMusicBot.Core.Attributes;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    public class AdminCommands : BaseCommandModule
    {
        [Command("deftest"), Aliases("dt")]
        [AdminCommand(CommandType.DefaultTest)]
        public async Task DefaultTestCommand(CommandContext ctx) { }
    }
}
