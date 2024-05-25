using EccomerceApi.Entity;

namespace EccomerceApi.Interfaces
{
    public interface IProductCategory
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(Guid id);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task<bool> UpdateAsync(Guid id, ProductCategory productCategory);
        Task<bool> DeleteAsync(Guid id);

    }
}
