namespace DiscordMusicBot.Core.Handlers
{
    using DiscordMusicBot.Commands.Enums;
    using DiscordMusicBot.Core.Attributes;
    using DiscordMusicBot.Core.Services.Interfaces;
    using DiscordMusicBot.Handlers;
    using DSharpPlus.CommandsNext;
    using System;
    using System.Threading.Tasks;

    public class AdminCommandHandler : BaseHandler
    {
        public AdminCommandHandler(IServiceProvider services) : base(services)
        {
        }

        public override async Task HandleCommands(CommandsNextExtension commands, CommandExecutionEventArgs args)
        {
            var cmd = args.Command;
            if (!IsAdminCommand(cmd))
            {
                return;
            }
            var type = GetAdminCommandType(cmd);
            var ctx = args.Context;

            var dbService = _services.GetService(typeof(IDatabaseService)) as IDatabaseService;
            var access = await dbService!.IdentifyMember(ctx);

            if(access != DB.Entities.Enums.AccessType.Admin)
            {
                await ctx.Channel.SendMessageAsync(
                    "Низкий уровень доступа для данной команды"
                    );
                return;
            }

            if(type == CommandType.DefaultTest)
            {
                await HandleDefaultTestCommand(ctx);
            }
        }

        private CommandType GetAdminCommandType(Command cmd)
        {
            var attr = cmd.CustomAttributes
                .Where(a => a is AdminCommandAttribute)
                .First() as AdminCommandAttribute;
            return attr!.Type;
        }

        private bool IsAdminCommand(Command cmd)
        {
            return cmd.CustomAttributes.Where(a => a is AdminCommandAttribute).Any();
        }

        private async Task HandleDefaultTestCommand(CommandContext ctx)
        {
            var text = ctx.Channel;
            var member = ctx.Member;

            await text.SendMessageAsync(
                "Проверка аттрибута администратора:\n" +
                $"Имя пользователя:\t{member.Username}\n" +
                $"Уровень доступа:\t{DB.Entities.Enums.AccessType.Admin}\n" +
                $"ID пользователя:\t{member.Id}\n" +
                $"Гильдия пользователя:\t{member.Guild.Name}\n" +
                $"ID гильдии пользователя:\t{member.Guild.Id}\n"
                );
        }
    }
}
