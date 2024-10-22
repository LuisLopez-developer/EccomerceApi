using Models;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProductCategory
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<List<ProductCategory>> SearchAsync(string name);
        Task<ProductCategory> GetByIdAsync(int id);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task<bool> UpdateAsync(int id, ProductCategory productCategory);
        Task<bool> DeleteAsync(int id);

    }
}
