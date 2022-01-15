namespace DiscordMusicBot.Core.Services.Interfaces
{
    using DiscordMusicBot.DB.Entities.Enums;
    using DSharpPlus.CommandsNext;

    public interface IDatabaseService
    {
        public Task<AccessType> IdentifyMember(CommandContext ctx);
    }
}
