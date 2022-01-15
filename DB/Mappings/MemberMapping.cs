namespace DiscordMusicBot.DB.Mappings
{
    using DiscordMusicBot.DB.Entities;
    using DiscordMusicBot.DB.Entities.Enums;
    using DiscordMusicBot.DB.Mappings.UserTypes;
    using FluentNHibernate.Mapping;

    public class MemberMapping : ClassMap<MemberEntity>
    {
        public MemberMapping()
        {
            Table("members");
            Id(x => x.Id);
            Map(x => x.UserId).Not.Nullable().CustomType<UInt64UserType>();
            Map(x => x.GuildId).Not.Nullable().CustomType<UInt64UserType>();
            Map(x => x.Access).CustomType<GenericEnumMapper<AccessType>>().Not.Nullable();
        }
    }
}
