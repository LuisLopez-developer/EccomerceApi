using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services
{
    public class ProductService : IProduct
    {
        private readonly IdentityDbContext _identityDbContext;

        public ProductService(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var productList = await _identityDbContext.Products
                .Include(p => p.IdStateNavigation)
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductBrand)
                .ToListAsync();

            var productViewModelList = productList.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Existence = product.Existence,
                StateName = product.IdStateNavigation?.Name,
                CategoryName = product.ProductCategory?.Name,
                BrandName = product.ProductBrand?.Name
            }).ToList();

            return productViewModelList;
        }


        public async Task<List<ProductViewModel>> SearchAsync(string name)
        {
            var matchedProducts = await _identityDbContext.Products
                .Where(product => product.Name.Contains(name))
                .Include(p => p.IdStateNavigation)
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductBrand)
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Existence = product.Existence,
                    StateName = product.IdStateNavigation.Name,
                    CategoryName = product.ProductCategory.Name,
                    BrandName = product.ProductBrand.Name
                })
                .ToListAsync();

            return matchedProducts;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _identityDbContext.Products.Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _identityDbContext.AddAsync(product);
            await _identityDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _identityDbContext.Products.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Code = product.Code;
                existingProduct.IdState = product.IdState;
                existingProduct.Date = product.Date;
                existingProduct.Price = product.Price;
                existingProduct.Cost = product.Cost;
                existingProduct.Existence = product.Existence;
                existingProduct.ProductBrandId = product.ProductBrandId;
                existingProduct.IdProductCategory = product.IdProductCategory;
            }

            await _identityDbContext.SaveChangesAsync();
            return existingProduct?.Id > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _identityDbContext.Products.Where(f => f.Id == id).FirstOrDefaultAsync();

            if(product != null)
            {
                _identityDbContext.Products.Remove(product);
                await _identityDbContext.SaveChangesAsync();
                id = product.Id;
            }

            return product?.Id > 0;
        }


    }
}
