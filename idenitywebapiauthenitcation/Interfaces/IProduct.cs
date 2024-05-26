using EccomerceApi.Entity;
using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;

namespace EccomerceApi.Interfaces
{
    public interface IProduct
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<List<ProductViewModel>> SearchAsync(string name);

        Task<ProductCreateModel> GetByIdAsync(int id);
        Task<ProductCreateModel> CreateAsync(ProductCreateModel product);
        Task<bool> UpdateAsync(int id, ProductCreateModel product);
        Task<bool> DeleteAsync(int id);
    }
}
