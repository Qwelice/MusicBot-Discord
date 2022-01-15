namespace DiscordMusicBot.Installation
{
    using System.Reflection;

    /// <summary>
    /// Базовый класс установщик
    /// </summary>
    public abstract class Installer
    {
        /// <summary>
        /// Сборка, внутри которой будет происходить установка
        /// </summary>
        protected Assembly _assembly = Assembly.GetExecutingAssembly();
    }
}
