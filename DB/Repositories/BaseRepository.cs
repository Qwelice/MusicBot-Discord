namespace DiscordMusicBot.DB.Repositories
{
    using DiscordMusicBot.DB.Repositories.Interfaces;
    using NHibernate;

    public abstract class BaseRepository<T> : IRepository<T>
    {
        private ISession _session;

        public BaseRepository(ISession session)
        {
            _session = session;
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public T Get(object id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> GetAll()
        {
            return _session.Query<T>();
        }

        public void SaveOrUpdate(T entity)
        {
            _session.SaveOrUpdate(entity);
        }
    }
}
