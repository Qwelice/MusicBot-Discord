namespace DiscordMusicBot.Handlers
{
    using DiscordMusicBot.Attributes;
    using DiscordMusicBot.Commands.Enums;
    using DiscordMusicBot.Core.Services.Interfaces;
    using DiscordMusicBot.Player;
    using DiscordMusicBot.Player.Payloads;
    using DiscordMusicBot.Services.Interfaces;
    using DiscordMusicBot.UI;
    using DSharpPlus.CommandsNext;
    using System;
    using System.Threading.Tasks;

    public class UserCommandHandler : BaseHandler
    {
        private MusicPlayer _player;
        public UserCommandHandler(IServiceProvider services) : base(services) 
        {
            _player = new MusicPlayer(services);
        }

        public override async Task HandleCommands(CommandsNextExtension commands, CommandExecutionEventArgs args)
        {
            var cmd = args.Command;
            if (!IsUserCommand(cmd))
            {
                return;
            }
            var type = GetUserCommandType(cmd);
            var ctx = args.Context;

            //var dbService = _services.GetService(typeof(IDatabaseService)) as IDatabaseService;
            //var access = await dbService!.IdentifyMember(ctx);



            if(type == CommandType.Join)
            {
                await HandleJoinCommand(ctx);
            }
            else if(type == CommandType.Leave)
            {
                await HandleLeaveCommand(ctx);
            }
            else if(type == CommandType.Play)
            {
                await HandlePlayCommand(ctx, ctx.RawArguments);
            }
            else if(type == CommandType.Pause)
            {
                await HandlePauseCommand(ctx);
            }
            else if(type == CommandType.Stop)
            {
                await HandleStopCommand(ctx);
            }
            else if (type == CommandType.Next)
            {
                await HandleNextCommand(ctx);
            }
            else if (type == CommandType.Back)
            {
                await HandleBackCommand(ctx);
            }
            else if (type == CommandType.Help)
            {

            }
            else if(type == CommandType.None)
            {
                await HandleNoneCommand(ctx);
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

        private async Task HandleJoinCommand(CommandContext ctx)
        {
            var connectionService = _services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService;
            await connectionService.ConnectToVoice(ctx, _services);
        }

        private async Task HandleLeaveCommand(CommandContext ctx)
        {
            var connectionService = _services.GetService(typeof(IVoiceConnectionService)) as IVoiceConnectionService;
            await connectionService.DisconnectFromVoice(ctx, _services);
        }

        private async Task HandlePlayCommand(CommandContext ctx, IReadOnlyList<string> args)
        {
            var query = args?.First();
            if (query == null)
            {
                await _player.AppendPayload(new PlayPayload(ctx));
            }
            else
            {
                await _player.AppendPayload(new QueryPayload(ctx, query));
            }
        }

        private async Task HandlePauseCommand(CommandContext ctx)
        {
            await _player.AppendPayload(new PausePayload(ctx));
        }

        private async Task HandleNextCommand(CommandContext ctx)
        {
            await _player.AppendPayload(new NextPayload(ctx));
        }

        private async Task HandleBackCommand(CommandContext ctx)
        {
            await _player.AppendPayload(new BackPayload(ctx));
        }

        private async Task HandleStopCommand(CommandContext ctx)
        {
            await _player.AppendPayload(new StopPayload(ctx));
        }

        private async Task HandleNoneCommand(CommandContext ctx)
        {
            // await BotUI.GetBotMenu().SendAsync(ctx.Channel);
            await ctx.Channel.SendMessageAsync(
                $"user_name: {ctx.Member.Username}\n" +
                $"user_nickname: {ctx.Member.Nickname}\n" +
                $"user_id: {ctx.Member.Id}\n" +
                $"user_is_bot: {ctx.Member.IsBot}\n" +
                $"user_guild_name: {ctx.Member.Guild.Name}\n" +
                $"user_guild_id: {ctx.Member.Guild.Id}\n" +
                $"user_guild_owner_name: {ctx.Member.Guild.Owner.Username}\n" +
                $"user_guild_owner_nickname: {ctx.Member.Guild.Owner.Nickname}\n" +
                $"user_guild_owner_id: {ctx.Member.Guild.Owner.Id}\n" +
                $"user_guild_owner_is_bot: {ctx.Member.Guild.Owner.IsBot}\n"
                );
        }
    }
}
