namespace DiscordMusicBot.Attributes
{
    using DiscordMusicBot.Commands.Enums;

    public class UserCommandAttribute : Attribute
    {
        public CommandType Type { get; private set; }

        public UserCommandAttribute(CommandType type)
        {
            Type = type;
        }
    }
}
