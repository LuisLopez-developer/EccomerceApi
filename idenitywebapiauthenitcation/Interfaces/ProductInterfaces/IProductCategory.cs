using Models;

namespace EccomerceApi.Interfaces.ProductIntefaces
{
    public interface IProductCategory
    {
        Task<List<ProductCategoryModel>> GetAllAsync();
        Task<List<ProductCategoryModel>> SearchAsync(string name);
        Task<ProductCategoryModel> GetByIdAsync(int id);
        Task<ProductCategoryModel> CreateAsync(ProductCategoryModel productCategory);
        Task<bool> UpdateAsync(int id, ProductCategoryModel productCategory);
        Task<bool> DeleteAsync(int id);

    }
}
