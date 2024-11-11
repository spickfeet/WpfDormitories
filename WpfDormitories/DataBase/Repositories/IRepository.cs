namespace WpfDormitories.DataBase.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        ICollection<T> Read();
        void Update(T entity);
        void Delete(T entity);
    }
}
