using Data.Entity;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProductBrand
    {
        Task<List<ProductBrand>> GetAllAsync();
        Task<List<ProductBrand>> SearchAsync(string name);
        Task<ProductBrand> GetByIdAsync(int id);
        Task<ProductBrand> CreateAsync(ProductBrand productBrand);
        Task<bool> UpdateAsync(int id, ProductBrand productBrand);
        Task<bool> DeleteAsync(int id);
    }
}
