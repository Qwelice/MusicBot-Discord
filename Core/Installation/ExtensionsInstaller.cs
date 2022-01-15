namespace DiscordMusicBot.Installation
{
    using DiscordMusicBot.Extensions.Interfaces;
    using DSharpPlus;

    public class ExtensionsInstaller : Installer
    {
        public void SetupExtensions(DiscordClient client)
        {
            var extList = _assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Extensions") && t.IsClass && t.Name.EndsWith("Extension"))
                .ToList();
            foreach (var extType in extList)
            {
                var ext = Activator.CreateInstance(extType) as IExtension;
                ext?.Setup(client);
            }
        }
    }
}
