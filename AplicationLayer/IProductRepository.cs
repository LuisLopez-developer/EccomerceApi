namespace AplicationLayer
{
    public interface IProductRepository<T>
    {
        Task<List<T>> GetByIdsAsync(IEnumerable<int> ids);
        Task<int> GetProductQuantityAsync(int productId);
    }
}
