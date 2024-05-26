using EccomerceApi.Entity;
using EccomerceApi.Model.ViewModel;

namespace EccomerceApi.Interfaces
{
    public interface IProduct
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<List<ProductViewModel>> SearchAsync(string name);

        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<bool> UpdateAsync(int id, Product product);
        Task<bool> DeleteAsync(int id);
    }
}
