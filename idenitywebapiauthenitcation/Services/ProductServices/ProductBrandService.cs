using Data;
using EccomerceApi.Interfaces.ProductIntefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EccomerceApi.Services.ProductServices
{
    public class ProductBrandService : IProductBrand
    {
        private readonly AppDbContext _identityDbContext;

        public ProductBrandService(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<ProductBrandModel> CreateAsync(ProductBrandModel productBrand)
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

        public async Task<List<ProductBrandModel>> GetAllAsync()
        {
            var producBrandsList = await _identityDbContext.ProductBrands.ToListAsync();
            return producBrandsList;
        }

        public async Task<ProductBrandModel> GetByIdAsync(int id)
        {
            return await _identityDbContext.ProductBrands.Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ProductBrandModel>> SearchAsync(string name)
        {
            // Filtrar las marcas de productos cuyo nombre contenga la cadena proporcionada
            var matchedBrands = await _identityDbContext.ProductBrands
                .Where(brands => brands.Name.Contains(name))
                .ToListAsync();

            return matchedBrands;
        }

        public async Task<bool> UpdateAsync(int id, ProductBrandModel productBrand)
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
