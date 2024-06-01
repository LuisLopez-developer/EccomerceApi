using EccomerceApi.Entity;
using EccomerceApi.Model.ProductModel.CreateModel;
using EccomerceApi.Model.ProductModel.EditModel;
using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProduct
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<List<ProductViewModel>> SearchAsync(string name);

        Task<ProductCreateModel> GetByIdAsync(int id);
        Task<ProductCreateModel> CreateAsync(ProductCreateModel product);
        Task<bool> UpdateAsync(int id, ProductEditModel product);
        Task<bool> DeleteAsync(int id);
    }
}
