namespace DiscordMusicBot.DB.Repositories.Interfaces
{
    using System.Linq;

    public interface IRepository<T>
    {
        void SaveOrUpdate(T entity);
        void Delete(T entity);
        T Get(object id);
        IQueryable<T> GetAll();
    }
}
