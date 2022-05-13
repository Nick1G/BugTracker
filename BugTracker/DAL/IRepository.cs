namespace BugTracker.DAL
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();

        T Get(int id);
        T Get(Func<T, bool> firstFunction);
        ICollection<T> GetAll();
        ICollection<T> GetList(Func<T, bool> whereFunction);
    }
}
