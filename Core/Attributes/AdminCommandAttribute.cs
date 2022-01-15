namespace DiscordMusicBot.Core.Attributes
{
    using DiscordMusicBot.Commands.Enums;

    public class AdminCommandAttribute : Attribute
    {
        public CommandType Type { get; private set; }

        public AdminCommandAttribute(CommandType type)
        {
            Type = type;
        }
    }
}
