namespace DiscordMusicBot.DB.Repositories
{
    using DiscordMusicBot.DB.Entities;
    using NHibernate;
    public class MemberRepository : BaseRepository<MemberEntity>
    {
        public MemberRepository(ISession session) : base(session)
        {
        }
    }
}
