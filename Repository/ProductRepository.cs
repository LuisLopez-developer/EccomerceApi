using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace Repository
{
    public class ProductRepository : IProductRepository<Product>, IRepository<Product>, IRepositorySearch<ProductModel, Product>
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAsync(Expression<Func<ProductModel, bool>> predicate)
        {
            var productModels = await _context.Products.Where(predicate).ToListAsync();
            var products = new List<Product>();

            foreach (var productModel in productModels)
            {
                var product = new Product(
                    productModel.Id,
                    productModel.Name,
                    productModel.SKU,
                    productModel.Date,
                    productModel.UpdateAt,
                    productModel.Cost,
                    productModel.Price,
                    productModel.Existence,
                    productModel.IsVisible,
                    productModel.Description ?? "",
                    productModel.BarCode,
                    productModel.StateId,
                    productModel.ProductBrandId,
                    productModel.ProductCategoryId);

                products.Add(product);
            }
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var productModel = await _context.Products
                .Include(p => p.ProductPhotos)
                .Include(p => p.ProductSpecifications)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (productModel == null)
            {
                return null; // Manejar el caso en que el producto no se encuentre
            }

            return new Product(
                productModel.Id,
                productModel.Name,
                productModel.SKU,
                productModel.Date,
                productModel.UpdateAt,
                productModel.Cost,
                productModel.Price,
                productModel.Existence,
                productModel.IsVisible,
                productModel.Description ?? "",
                productModel.BarCode,
                productModel.StateId,
                productModel.ProductBrandId,
                productModel.ProductCategoryId,
                productModel.ProductPhotos.Select(pp => new ProductPhoto(
                    pp.Id, 
                    pp.FileName, 
                    pp.Url,
                    pp.IsMain)).ToList()
            );
        }

        public async Task<List<Product>> GetByIdsAsync(IEnumerable<int> ids)
        {
            // recuperar los productos con sus fotos
            var productModels = await _context.Products
                .Include(p => p.ProductPhotos)
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();

            var products =  productModels.Select(p => new Product(
                p.Id,
                p.Name,
                p.SKU,
                p.Date,
                p.UpdateAt,
                p.Cost,
                p.Price,
                p.Existence,
                p.IsVisible,
                p.Description ?? "",
                p.BarCode,
                p.StateId,
                p.ProductBrandId,
                p.ProductCategoryId,
                p.ProductPhotos.Select(pp => new ProductPhoto(
                    pp.Id,
                    pp.FileName,
                    pp.Url,
                    pp.IsMain)).ToList()
            )).ToList();

            return products;
        }

        public async Task<int> GetProductQuantityAsync(int productId)
            => await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Existence)
                .FirstOrDefaultAsync();
        

        public async Task UpdateAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct == null)
                throw new InvalidOperationException("El producto no existe.");

            // Ahora, solo actualizar los campos que son diferentes
            if (!string.IsNullOrEmpty(product.Name) && product.Name != existingProduct.Name)
                existingProduct.Name = product.Name;

            if (!string.IsNullOrEmpty(product.SKU) && product.SKU != existingProduct.SKU)
                existingProduct.SKU = product.SKU;

            if (product.Price != existingProduct.Price)
                existingProduct.Price = product.Price;

            if (product.Cost != existingProduct.Cost)
                existingProduct.Cost = product.Cost;

            if (product.Existence != existingProduct.Existence)
                existingProduct.Existence = product.Existence;

            if (product.IsVisible != existingProduct.IsVisible)
                existingProduct.IsVisible = product.IsVisible;

            if (!string.IsNullOrEmpty(product.Description) && product.Description != existingProduct.Description)
                existingProduct.Description = product.Description;

            if (!string.IsNullOrEmpty(product.BarCode) && product.BarCode != existingProduct.BarCode)
                existingProduct.BarCode = product.BarCode;

            if (product.StateId != existingProduct.StateId)
                existingProduct.StateId = product.StateId;

            if (product.ProductBrandId != existingProduct.ProductBrandId)
                existingProduct.ProductBrandId = product.ProductBrandId;

            if (product.ProductCategoryId != existingProduct.ProductCategoryId)
                existingProduct.ProductCategoryId = product.ProductCategoryId;

            await _context.SaveChangesAsync();

        }
    }
}
