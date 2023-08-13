namespace Contracts.Common
{
    public interface IGenericRepository<T>
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
