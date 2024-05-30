using Microsoft.EntityFrameworkCore;
using EccomerceApi.Entity;
using EccomerceApi.Data;
using EccomerceApi.Interfaces.Product;

namespace EccomerceApi.Services.Product
{
    public class ProductBrandService : IProductBrand
    {
        private readonly IdentityDbContext _identityDbContext;

        public ProductBrandService(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<ProductBrand> CreateAsync(ProductBrand productBrand)
        {
            await _identityDbContext.AddAsync(productBrand);
            await _identityDbContext.SaveChangesAsync();
            return productBrand;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productBrand = await _identityDbContext.ProductBrands.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (productBrand != null)
            {
                _identityDbContext.ProductBrands.Remove(productBrand);
                await _identityDbContext.SaveChangesAsync();
                id = productBrand.Id;
            }

            return productBrand?.Id > 0;
        }

        public async Task<List<ProductBrand>> GetAllAsync()
        {
            var producBrandsList = await _identityDbContext.ProductBrands.ToListAsync();
            return producBrandsList;
        }

        public async Task<ProductBrand> GetByIdAsync(int id)
        {
            return await _identityDbContext.ProductBrands.Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ProductBrand>> SearchAsync(string name)
        {
            // Filtrar las marcas de productos cuyo nombre contenga la cadena proporcionada
            var matchedBrands = await _identityDbContext.ProductBrands
                .Where(brands => brands.Name.Contains(name))
                .ToListAsync();

            return matchedBrands;
        }

        public async Task<bool> UpdateAsync(int id, ProductBrand productBrand)
        {
            var existingProductBrand = await _identityDbContext.ProductBrands.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (existingProductBrand != null)
            {
                existingProductBrand.Name = productBrand.Name;
            }

            await _identityDbContext.SaveChangesAsync();

            return existingProductBrand?.Id > 0;
        }
    }
}
