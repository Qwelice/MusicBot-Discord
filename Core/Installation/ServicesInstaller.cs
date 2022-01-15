namespace DiscordMusicBot.Installation
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Установщик сервисов
    /// </summary>
    public class ServicesInstaller : Installer
    {
        /// <summary>
        /// Создание коллекции сервисов
        /// </summary>
        /// <returns>Провайдер сервисов</returns>
        public IServiceProvider SetupAndGetServices()
        {
            var collection = new ServiceCollection();
            FillCollection(collection);
            return collection.BuildServiceProvider();
        }

        /// <summary>
        /// Создание коллекции сервисов
        /// </summary>
        /// <param name="services">Провайдер, который необходимо наполнить сервисами</param>
        public void SetupServices(ref IServiceProvider services)
        {
            var collection = new ServiceCollection();
            FillCollection(collection);
            services = collection.BuildServiceProvider();
        }

        /// <summary>
        /// Наполнение сервисами коллекции сервисов
        /// </summary>
        /// <param name="collection">Наполняемая коллекция</param>
        private void FillCollection(ServiceCollection collection)
        {
            // Список типов, отвечающих за сервисы (они обязательно должны лежать в пространствах, имена которых заканчиваются на Services.Interfaces)
            var typeList = _assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Services.Interfaces")
                        && t.IsInterface)
                .ToList();
            // Список реализаций типов, отвечающих за сервисы (они обязательно должны лежать в пространствах, имена которых заканчиваются на Services)
            var implList = _assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Services")
                        && t.IsClass)
                .ToList();
            // Добавление сервисов в коллекцию
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
