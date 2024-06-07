using EccomerceApi.Entity;
using EccomerceApi.Model;
using EccomerceApi.Model.ProductModel.CreateModel;
using EccomerceApi.Model.ProductModel.EditModel;
using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProduct
    {
        Task<PagedResult<ProductViewModel>> GetLeakedProductsAsync(int page, int pageSize, string searchTerm, DateTime? startDate, DateTime? endDate);

        Task<List<ProductViewModel>> GetAllAsync();
        Task<List<ProductViewModel>> SearchAsync(string name);

        Task<ProductCreateModel> GetByIdAsync(int id);
        Task<ProductCreateModel> CreateAsync(ProductCreateModel product);
        Task<bool> UpdateAsync(int id, ProductCreateModel product);
        Task<bool> ChangeStateAsync(int id);
        Task<bool> DeleteAsync(int id);

        // Metodos, enfocados para el eccomerce
        Task<List<ProductCatalogViewModel>> GetMostValuableProductCatalog(); 
    }
}
