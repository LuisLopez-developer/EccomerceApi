using EccomerceApi.Model.ProductModel.CreateModel;
using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProductPhoto
    {
        Task<List<ProductPhotoViewModel>> SearchByProductIdAsync(int productId);
        Task<ProductPhotoViewModel> CreateAsync(int productId, ProductPhotoViewModel productPhoto);
        Task<bool> UpdateAsync(int id, ProductPhotoViewModel productPhoto);
        Task<bool> DeleteAsync(int id);

    }
}
