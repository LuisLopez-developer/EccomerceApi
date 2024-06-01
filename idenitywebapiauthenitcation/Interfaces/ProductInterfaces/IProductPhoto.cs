using EccomerceApi.Entity;
using EccomerceApi.Model.ProductModel.CreateModel;
using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProductPhoto
    {
        Task<List<ProductPhotoViewModel>> SearchByProductIdAsync(int productId);
        Task<List<ProductPhotoViewModel>> CreateAsync(int productId, List<ProductPhotoViewModel> productPhoto);
        Task<bool> UpdateAsync(int id, ProductPhotoViewModel productPhoto);
        Task<bool> UpdateProductPhotosAsync(int productId, List<ProductPhotoViewModel> productPhotos);
        Task<bool> DeleteAsync(int id);
    }
}
