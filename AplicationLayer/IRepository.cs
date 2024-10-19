namespace AplicationLayer
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
