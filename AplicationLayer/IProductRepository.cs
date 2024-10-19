namespace AplicationLayer
{
    public interface IProductRepository<T>
    {
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
