using Models;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProductBrand
    {
        Task<List<ProductBrandModel>> GetAllAsync();
        Task<List<ProductBrandModel>> SearchAsync(string name);
        Task<ProductBrandModel> GetByIdAsync(int id);
        Task<ProductBrandModel> CreateAsync(ProductBrandModel productBrand);
        Task<bool> UpdateAsync(int id, ProductBrandModel productBrand);
        Task<bool> DeleteAsync(int id);
    }
}
