namespace DiscordMusicBot.Installation
{
    using System.Reflection;

    public abstract class Installer
    {
        protected Assembly _assembly = Assembly.GetExecutingAssembly();
    }
}
