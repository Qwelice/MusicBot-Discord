namespace DiscordMusicBot.DB.Entities
{
    using DiscordMusicBot.DB.Entities.Enums;

    public class MemberEntity : BaseEntity
    {
        public virtual ulong UserId { get; set; }
        public virtual AccessType Access { get; set; }
        public virtual ulong GuildId { get; set; }
    }
}
