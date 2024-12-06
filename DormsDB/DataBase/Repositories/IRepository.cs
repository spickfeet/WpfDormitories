namespace WpfDormitories.DataBase.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        IList<T> Read();
        void Update(T entity);
        void Delete(T entity);
    }
}
