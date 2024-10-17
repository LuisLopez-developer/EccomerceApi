using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Interfaces.ProductInterfaces
{
    public interface IProductSpecification
    {
        Task<ProductSpecificationViewModel> SearchByProductIdAsync(int productId);
        Task<ProductSpecificationViewModel> CreateAsync(int productId, ProductSpecificationViewModel productSpecification);
        Task<bool> UpdateAsync(int id, ProductSpecificationViewModel productSpecification);
        Task<bool> DeleteAsync(int id);
    }
}
