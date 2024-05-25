using EccomerceApi.Data;
using EccomerceApi.Interfaces;
using EccomerceApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services
{
    public class ProductCategoryService : IProductCategory
    {
        private readonly IdentityDbContext _identityDbContext;

        public ProductCategoryService(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            var producCategorytList = await _identityDbContext.ProductCategories.ToListAsync();
            return producCategorytList;
        }

        public async Task<ProductCategory> GetByIdAsync(Guid id)
        {
            return await _identityDbContext.ProductCategories.Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory productCategory)
        {
            await _identityDbContext.AddAsync(productCategory);
            await _identityDbContext.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> UpdateAsync(Guid id, ProductCategory productCategory)
        {
            var existingProductCategory = await _identityDbContext.ProductCategories.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (existingProductCategory != null)
            {
                existingProductCategory.Name = productCategory.Name;
            }

            await _identityDbContext.SaveChangesAsync();

            return existingProductCategory?.Id != Guid.Empty;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var productCategory = await _identityDbContext.ProductCategories.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (productCategory != null)
            {
                _identityDbContext.ProductCategories.Remove(productCategory);
                await _identityDbContext.SaveChangesAsync();
                id = productCategory.Id;
            }

            return productCategory?.Id != Guid.Empty;
        }
    }
}
