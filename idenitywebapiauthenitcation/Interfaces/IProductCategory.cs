using EccomerceApi.Entity;

namespace EccomerceApi.Interfaces
{
    public interface IProductCategory
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(int id);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task<bool> UpdateAsync(int id, ProductCategory productCategory);
        Task<bool> DeleteAsync(int id);

    }
}
