using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Interfaces.ProductInterfaces;
using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ProductModel.CreateModel;
using EccomerceApi.Model.ProductModel.EditModel;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services.ProductServices
{
    public class ProductService : IProduct
    {
        private readonly IdentityDbContext _identityDbContext;

        private readonly IProductPhoto _productPhotoService;
        private readonly IProductSpecification _productSpecificationService;

        public ProductService(IdentityDbContext identityDbContext, IProductPhoto productPhoto, IProductSpecification productSpecification)
        {
            _identityDbContext = identityDbContext;
            _productPhotoService = productPhoto;
            _productSpecificationService = productSpecification;
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
            var product = await _identityDbContext.Products
                .Include(p => p.ProductPhotos)
                .Include(p => p.ProductSpecifications)
                .FirstOrDefaultAsync(p => p.Id == id);

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
                ProductCategoryId = product.ProductCategoryId,
                Photos = product.ProductPhotos?.Select(photo => new ProductPhotoViewModel
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    IsMain = photo.IsMain,
                    ProductId = photo.ProductId
                }).ToList(),
                Specifications = product.ProductSpecifications == null ? null : new ProductSpecificationViewModel
                {
                    ProductId = product.ProductSpecifications.ProductId,
                    Color = product.ProductSpecifications.Color,
                    Sensor = product.ProductSpecifications.Sensor,
                    ModelNumber = product.ProductSpecifications.ModelNumber,
                    ProcessorSpeed = product.ProductSpecifications.ProcessorSpeed,
                    ScreenSize = product.ProductSpecifications.ScreenSize,
                    ScreenResolution = product.ProductSpecifications.ScreenResolution,
                    ScreenTechnology = product.ProductSpecifications.ScreenTechnology,
                    RearCameraResolution = product.ProductSpecifications.RearCameraResolution,
                    FrontCameraResolution = product.ProductSpecifications.FrontCameraResolution,
                    RAM = product.ProductSpecifications.RAM,
                    InternalStorage = product.ProductSpecifications.InternalStorage,
                    SimType = product.ProductSpecifications.SimType,
                    SimCount = product.ProductSpecifications.SimCount,
                    NFC = product.ProductSpecifications.NFC,
                    BluetoothVersion = product.ProductSpecifications.BluetoothVersion,
                    UsbInterface = product.ProductSpecifications.UsbInterface,
                    OperatingSystem = product.ProductSpecifications.OperatingSystem,
                    BatteryCapacity = product.ProductSpecifications.BatteryCapacity,
                    Waterproof = product.ProductSpecifications.Waterproof,
                    WaterResistanceRating = product.ProductSpecifications.WaterResistanceRating,
                    SplashResistant = product.ProductSpecifications.SplashResistant
                }

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
            var newProduct = new Entity.Product // el Entity no deberia ser necesario usando using, pero me marcaba como error de tipo
            {
                Name = productCreateModel.Name,
                Code = productCreateModel.Code, //SKU
                StateId = productCreateModel.StateId,
                Date = productCreateModel.Date,
                UpdateAt = productCreateModel.Date ?? DateTime.UtcNow, // Para la creación la fecha UpdateAt sera igual que a la de la creación
                Price = productCreateModel.Price,
                Cost = productCreateModel.Cost,
                Existence = productCreateModel.Existence,
                IsVisible = productCreateModel.IsVisible,
                Description = productCreateModel.Description,
                BarCode = productCreateModel.BarCode,
                ProductBrandId = productCreateModel.ProductBrandId,
                ProductCategoryId = productCreateModel.ProductCategoryId
            };

            // Agregar el nuevo producto a la base de datos
            _identityDbContext.Products.Add(newProduct);
            await _identityDbContext.SaveChangesAsync(); // Aquí se asigna el ID del producto nuevo

            // Crear las fotos del producto si hay alguna
            if (productCreateModel.Photos != null && productCreateModel.Photos.Any())
            {
                try
                {
                    await _productPhotoService.CreateAsync(newProduct.Id, productCreateModel.Photos);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al crear fotos del producto: " + ex.Message, ex);
                }
            }

            // Crear las especificaciones del producto si existen
            if (productCreateModel.Specifications != null)
            {
                try
                {
                    await _productSpecificationService.CreateAsync(newProduct.Id, productCreateModel.Specifications);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al crear especificaciones del producto: " + ex.Message, ex);
                }
            }


            // Devolver el objeto ProductCreateModel recién creado
            return productCreateModel;
        }

        public async Task<bool> UpdateAsync(int id, ProductEditModel product)
        {
            var existingProduct = await _identityDbContext.Products
                .Include(p => p.ProductPhotos)
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Code = product.Code;
                existingProduct.StateId = product.StateId;

                // Obtener la hora de Perú
                TimeZoneInfo peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Lima");
                DateTime peruTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);

                existingProduct.UpdateAt = peruTime;
                existingProduct.Price = product.Price;
                existingProduct.Cost = product.Cost;
                existingProduct.Existence = product.Existence;
                existingProduct.ProductBrandId = product.ProductBrandId;
                existingProduct.ProductCategoryId = product.ProductCategoryId;

                // Actualizar las fotos del producto
                if (product.Photos != null && product.Photos.Any())
                {
                    await _productPhotoService.UpdateProductPhotosAsync(existingProduct.Id, product.Photos);
                }

                // Actualizar las especificaciones del producto
                if (product.Specifications != null)
                {
                    await _productSpecificationService.UpdateAsync(existingProduct.Id, product.Specifications);
                }

                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
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
