using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : IProductRepository<Product>
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var productModels = await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();

            return productModels.Select(productModel => new Product(
                productModel.Id,
                productModel.Name,
                productModel.SKU,
                productModel.Date,
                productModel.UpdateAt,
                productModel.Price,
                productModel.Cost,
                productModel.Existence,
                productModel.IsVisible,
                productModel.Description ?? "",
                productModel.BarCode,
                productModel.StateId,
                productModel.ProductBrandId,
                productModel.ProductCategoryId));
        }
    }
}
