using Data;
using EccomerceApi.Interfaces.ProductIntefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EccomerceApi.Services.ProductServices
{
    public class ProductCategoryService : IProductCategory
    {
        private readonly AppDbContext _identityDbContext;

        public ProductCategoryService(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<List<ProductCategoryModel>> SearchAsync(string name)
        {
            // Filtrar las categorías de productos cuyo nombre contenga la cadena proporcionada
            var matchedCategories = await _identityDbContext.ProductCategories
                .Where(category => category.Name.Contains(name))
                .ToListAsync();

            return matchedCategories;
        }

        public async Task<List<ProductCategoryModel>> GetAllAsync()
        {
            var producCategorytList = await _identityDbContext.ProductCategories.ToListAsync();
            return producCategorytList;
        }

        public async Task<ProductCategoryModel> GetByIdAsync(int id)
        {
            return await _identityDbContext.ProductCategories.Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductCategoryModel> CreateAsync(ProductCategoryModel productCategory)
        {
            await _identityDbContext.AddAsync(productCategory);
            await _identityDbContext.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> UpdateAsync(int id, ProductCategoryModel productCategory)
        {
            var existingProductCategory = await _identityDbContext.ProductCategories.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (existingProductCategory != null)
            {
                existingProductCategory.Name = productCategory.Name;
            }

            await _identityDbContext.SaveChangesAsync();

            return existingProductCategory?.Id > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productCategory = await _identityDbContext.ProductCategories.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (productCategory != null)
            {
                _identityDbContext.ProductCategories.Remove(productCategory);
                await _identityDbContext.SaveChangesAsync();
                id = productCategory.Id;
            }

            return productCategory?.Id > 0;
        }
    }
}
