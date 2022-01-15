namespace DiscordMusicBot.DB.NH
{
    using DiscordMusicBot.Configurations;
    using DiscordMusicBot.Configurations.Models;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;
    using System;
    using System.Reflection;

    public static class Helper
    {
        private static Configuration _config;
        private static ISessionFactory _factory;

        public static Configuration Config
        {
            get
            {
                return _config ?? (_config = CreateConfig());
            }
        }

        public static ISessionFactory Factory
        {
            get
            {
                return _factory ?? (_factory = Config.BuildSessionFactory());
            }
        }

        public static void SessionContext(Action<ISession> action)
        {
            using (var session = Factory.OpenSession())
            {
                using (var tr = session.BeginTransaction())
                {
                    action(session);
                    tr.Commit();
                }
            }
        }

        public static void SetSchema()
        {
            new SchemaExport(Config).Create(true, true);
        }

        private static Configuration CreateConfig()
        {
            DBConfiguration cfg = Configurator.GetConfiguration<DBConfiguration>();
            return Fluently.Configure()
                .Database(
                    PostgreSQLConfiguration
                    .Standard
                    .ConnectionString(x => x
                        .Host(cfg.Host)
                        .Username(cfg.Username)
                        .Password(cfg.Password)
                        .Database(cfg.DatabaseName)
                        .Port(cfg.Port)
                    )
                    .Dialect<NHibernate.Dialect.PostgreSQL83Dialect>()
                    .Driver<NHibernate.Driver.NpgsqlDriver>()
                ).Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildConfiguration();
        }
    }
}
