namespace DiscordMusicBot.Configurations
{
    using DiscordMusicBot.Configurations.Models;
    using System;
    using System.Text.Json;

    public static class Configurator
    {
        private static string ConfigDir
        {
            get
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var projectDir = Directory.GetParent(baseDir)?.Parent?.Parent?.Parent?.FullName;
                if(projectDir != null)
                {
                    return @$"{projectDir}\configs";
                }
                else
                {
                    return baseDir;
                }
            }
        }
        
        public static T GetConfiguration<T>()
        {
            T cfg;
            using(FileStream fs = new FileStream(@$"{ConfigDir}\{typeof(T).Name}.json", FileMode.OpenOrCreate))
            {
                cfg = JsonSerializer.Deserialize<T>(fs)!;
            }
            return cfg;
        }
    }
}
