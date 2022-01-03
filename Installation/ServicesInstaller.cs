namespace DiscordMusicBot.Installation
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    public class ServicesInstaller : Installer
    {
        public IServiceProvider SetupAndGetServices()
        {
            var collection = new ServiceCollection();
            FillCollection(collection);
            return collection.BuildServiceProvider();
        }

        public void SetupServices(ref IServiceProvider services)
        {
            var collection = new ServiceCollection();
            FillCollection(collection);
            services = collection.BuildServiceProvider();
        }

        private void FillCollection(ServiceCollection collection)
        {
            var typeList = _assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Services.Interfaces")
                        && t.IsInterface)
                .ToList();
            var implList = _assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Services")
                        && t.IsClass)
                .ToList();
            for (int i = 0; i < typeList.Count; i++)
            {
                for (int j = 0; j < implList.Count; j++)
                {
                    if (typeList[i].Name.EndsWith(implList[j].Name))
                    {
                        collection.AddScoped(typeList[i], implList[j]);
                        break;
                    }
                }
            }
        }
    }
}
