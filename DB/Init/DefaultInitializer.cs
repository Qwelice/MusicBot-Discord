namespace DiscordMusicBot.DB.Init
{
    using DiscordMusicBot.DB.Entities;
    using DiscordMusicBot.DB.NH;
    using DiscordMusicBot.DB.Repositories;
    using DSharpPlus;
    using System.Linq;

    public static class DefaultInitializer
    {
        public static void Seed(bool isNewSchema, DiscordClient client)
        {
            if (isNewSchema)
            {
                Helper.SetSchema();
            }

            Helper.SessionContext(session =>
            {
                var memberRepo = new MemberRepository(session);
                var guilds = client.Guilds;
                foreach(var gid in guilds.Keys)
                {
                    var guildMemberEntities = memberRepo.GetAll().Where(m => m.GuildId == gid).ToList();
                    var guildMembers = guilds[gid].Members.Values.Where(m => !m.IsBot).ToList();
                    if(guildMemberEntities.Count != guildMembers.Count)
                    {
                        foreach(var member in guildMembers)
                        {
                            if(!guildMemberEntities.Any(e => e.UserId == member.Id))
                            {
                                var m = new MemberEntity
                                {
                                    UserId = member.Id,
                                    GuildId = gid,
                                    Access = Entities.Enums.AccessType.Default
                                };
                                memberRepo.SaveOrUpdate(m);
                            }
                        }
                    }
                }
            });
        }
    }
}
