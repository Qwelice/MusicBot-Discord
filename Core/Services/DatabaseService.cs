namespace DiscordMusicBot.Core.Services
{
    using DiscordMusicBot.Core.Services.Interfaces;
    using DiscordMusicBot.DB.Entities;
    using DiscordMusicBot.DB.Entities.Enums;
    using DiscordMusicBot.DB.NH;
    using DiscordMusicBot.DB.Repositories;
    using DSharpPlus.CommandsNext;

    public class DatabaseService : IDatabaseService
    {
        public Task<AccessType> IdentifyMember(CommandContext ctx)
        {
            return Task<AccessType>.Factory.StartNew(() =>
            {
                var member = ctx.Member;
                AccessType access = AccessType.Default;
                Helper.SessionContext(session =>
                {
                    var memberRepo = new MemberRepository(session);
                    var memberEntities = memberRepo.GetAll().Where(m => m.GuildId == member.Guild.Id).ToList();
                    if(memberEntities.Any(m => m.UserId == member.Id))
                    {
                        var entity = memberEntities.Where(m => m.UserId == member.Id).First();
                        access = entity.Access;
                    }
                    else
                    {
                        var entity = new MemberEntity
                        {
                            UserId = member.Id,
                            GuildId = member.Guild.Id,
                            Access = access
                        };
                        memberRepo.SaveOrUpdate(entity);
                    }
                });

                return access;
            });
        }
    }
}
