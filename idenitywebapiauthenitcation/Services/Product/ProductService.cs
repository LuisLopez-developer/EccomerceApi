using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Interfaces.Product;
using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services.Product
{
    public class ProductService : IProduct
    {
        private readonly IdentityDbContext _identityDbContext;

        private readonly IEntry _entryService;

        public ProductService(IdentityDbContext identityDbContext, IEntry entryService)
        {
            _identityDbContext = identityDbContext;
            _entryService = entryService;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var productList = await _identityDbContext.Products
                .Include(p => p.State)
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductBrand)
                .ToListAsync();

            var productViewModelList = productList.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Existence = product.Existence,
                StateName = product.State?.Name,
                CategoryName = product.ProductCategory?.Name,
                BrandName = product.ProductBrand?.Name,
                Price = product.Price,
                Cost = product.Cost
            }).ToList();

            return productViewModelList;
        }


        public async Task<List<ProductViewModel>> SearchAsync(string name)
        {
            var matchedProducts = await _identityDbContext.Products
                .Where(product => product.Name.Contains(name))
                .Include(p => p.State)
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductBrand)
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.ProductCategory.Name,
                    BrandName = product.ProductBrand.Name,
                    StateName = product.State.Name,
                    Existence = product.Existence,
                    Cost = product.Cost,
                    Price = product.Price
                })
                .ToListAsync();

            return matchedProducts;
        }

        public async Task<ProductCreateModel> GetByIdAsync(int id)
        {
            var product = await _identityDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            var productCreateModel = new ProductCreateModel
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                StateId = product.StateId,
                Date = product.Date,
                Price = product.Price,
                Cost = product.Cost,
                Existence = product.Existence,
                ProductBrandId = product.ProductBrandId,
                ProductCategoryId = product.ProductCategoryId
            };

            return productCreateModel;
        }

        public async Task<ProductCreateModel> CreateAsync(ProductCreateModel productCreateModel)
        {
            // Si la fecha es nula, establece la fecha actual
            if (productCreateModel.Date == null)
            {
                // Obtener la hora actual de Perú
                var peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                var currentTimePeru = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);

                productCreateModel.Date = currentTimePeru;
            }

            // Crear un nuevo objeto Product a partir de ProductCreateModel
            var newProduct = new Product
            {
                Name = productCreateModel.Name,
                Code = productCreateModel.Code,
                StateId = productCreateModel.StateId,
                Date = productCreateModel.Date,
                Price = productCreateModel.Price,
                Cost = productCreateModel.Cost,
                Existence = productCreateModel.Existence,
                ProductBrandId = productCreateModel.ProductBrandId,
                ProductCategoryId = productCreateModel.ProductCategoryId
            };

            // Agregar el nuevo producto a la base de datos
            _identityDbContext.Products.Add(newProduct);
            await _identityDbContext.SaveChangesAsync(); // Aquí se asigna el ID del producto nuevo

            // Crear una nueva entrada correspondiente al nuevo producto
            var entryCreateModel = new EntryCreateModel
            {
                Date = productCreateModel.Date,
                Total = newProduct.Cost * newProduct.Existence,
                IdState = newProduct.StateId,
                UnitCost = newProduct.Cost,
                Amount = newProduct.Existence,
                IdProduct = newProduct.Id // Usamos el ID del producto recién creado
            };

            await _entryService.CreateAsync(entryCreateModel);

            // Devolver el objeto ProductCreateModel recién creado
            return productCreateModel;
        }

        public async Task<bool> UpdateAsync(int id, ProductCreateModel product)
        {
            var existingProduct = await _identityDbContext.Products.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Code = product.Code;
                existingProduct.StateId = product.StateId;
                existingProduct.Date = product.Date;
                existingProduct.Price = product.Price;
                existingProduct.Cost = product.Cost;
                existingProduct.Existence = product.Existence;
                existingProduct.ProductBrandId = product.ProductBrandId;
                existingProduct.ProductCategoryId = product.ProductCategoryId;
            }

            await _identityDbContext.SaveChangesAsync();
            return existingProduct?.Id > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _identityDbContext.Products.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (product != null)
            {
                _identityDbContext.Products.Remove(product);
                await _identityDbContext.SaveChangesAsync();
                id = product.Id;
            }

            return product?.Id > 0;
        }


    }
}
