using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Herlpers;
using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Interfaces.ProductInterfaces;
using EccomerceApi.Model;
using EccomerceApi.Model.ProductModel.CreateModel;
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
                SKU = product.SKU,
                StateId = product.StateId,
                BarCode = product.BarCode,
                Date = product.UpdateAt,
                IsVisible = product.IsVisible,
                Description = product.Description,
                Price = product.Price,
                Cost = product.Cost,
                Existence = product.Existence,
                ProductBrandId = product.ProductBrandId,
                ProductCategoryId = product.ProductCategoryId,
                Photos = product.ProductPhotos?.Select(photo => new ProductPhotoViewModel
                {
                    Id = photo.Id,
                    FileName = photo.FileName,
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
            try
            {
                var newProduct = new Product
                {
                    Name = productCreateModel.Name,
                    SKU = productCreateModel.SKU,
                    StateId = productCreateModel.StateId,
                    Date = productCreateModel.Date,
                    UpdateAt = productCreateModel.Date,
                    Price = productCreateModel.Price,
                    Cost = productCreateModel.Cost,
                    Existence = productCreateModel.Existence,
                    IsVisible = productCreateModel.IsVisible,
                    Description = productCreateModel.Description,
                    BarCode = productCreateModel.BarCode,
                    ProductBrandId = productCreateModel.ProductBrandId,
                    ProductCategoryId = productCreateModel.ProductCategoryId
                };

                _identityDbContext.Products.Add(newProduct);
                await _identityDbContext.SaveChangesAsync();


                // Se guarda la información de las fotos, si existe alguna
                if (productCreateModel.Photos != null && productCreateModel.Photos.Any())
                {
                    await _productPhotoService.CreateAsync(newProduct.Id, productCreateModel.Photos);
                }

                // Se guarda la información de las especificaciones, si existe alguna
                if (productCreateModel.Specifications != null)
                {
                    await _productSpecificationService.CreateAsync(newProduct.Id, productCreateModel.Specifications);
                }

                return productCreateModel;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear el producto: " + ex.Message, ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, ProductCreateModel product)
        {            try
            {
                var existingProduct = await _identityDbContext.Products
                    .Include(p => p.ProductPhotos)
                    .Include(sp => sp.ProductSpecifications)
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.SKU = product.SKU;
                    existingProduct.StateId = product.StateId;
                    existingProduct.UpdateAt = getTimePeruHelper.GetCurrentTimeInPeru();
                    existingProduct.Price = product.Price;
                    existingProduct.Cost = product.Cost;
                    existingProduct.Existence = product.Existence;
                    existingProduct.IsVisible = product.IsVisible;
                    existingProduct.Description = product.Description;
                    existingProduct.BarCode = product.BarCode;
                    existingProduct.ProductBrandId = product.ProductBrandId;
                    existingProduct.ProductCategoryId = product.ProductCategoryId;

                    if (product.Photos != null && product.Photos.Any())
                    {
                        await _productPhotoService.UpdateProductPhotosAsync(existingProduct.Id, product.Photos);
                    }

                    if (product.Specifications != null)
                    {
                        await _productSpecificationService.UpdateAsync(existingProduct.Id, product.Specifications);
                    }

                    await _identityDbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar el producto: " + ex.Message, ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var transaction = await _identityDbContext.Database.BeginTransactionAsync();
            try
            {
                var product = await _identityDbContext.Products.Where(f => f.Id == id).FirstOrDefaultAsync();

                if (product != null)
                {
                    _identityDbContext.Products.Remove(product);
                    await _identityDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Error al eliminar el producto: " + ex.Message, ex);
            }
        }

        public async Task<bool> ChangeStateAsync(int id)
        {
            var product = await _identityDbContext.Products.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (product != null)
            {
                product.StateId = 2; // 2 = Inactivo
                product.UpdateAt = getTimePeruHelper.GetCurrentTimeInPeru(); // Actualizar la fecha de cambio
                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<PagedResult<ProductViewModel>> GetLeakedProductsAsync(int page, int pageSize, string searchTerm, DateTime? startDate, DateTime? endDate)
        {
            var query = _identityDbContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductCategory)
                .Include(p => p.State)
                .AsQueryable();

            // Aplicar filtros por fechas si se proporcionan
            if (startDate.HasValue)
            {
                DateTime startOfDay = startDate.Value.Date;
                query = query.Where(p => p.Date >= startOfDay);
            }

            if (endDate.HasValue)
            {
                // Incrementar la fecha en un día para incluir registros hasta el final del día
                DateTime endOfDay = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(p => p.Date <= endOfDay);
            }

            // Aplicar filtro de término de búsqueda si se proporciona
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) ||
                                         p.ProductBrand.Name.Contains(searchTerm) ||
                                         p.ProductCategory.Name.Contains(searchTerm));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var products = await query
                .OrderBy(p => p.State.Id == 1 ? 0 : p.State.Id == 2 ? 2 : 1) // Ordenar por estado (1, otros, 2)
                .ThenByDescending(p => p.UpdateAt) // Luego por fecha de actualización descendente
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Existence = p.Existence,
                    CategoryName = p.ProductCategory.Name,
                    BrandName = p.ProductBrand.Name,
                    Price = p.Price,
                    Cost = p.Cost,
                    StateName = p.State.Name
                })
                .ToListAsync();

            return new PagedResult<ProductViewModel>
            {
                Items = products,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        // Metodos, enfocados para el eccomerce
        public async Task<List<ProductCatalogViewModel>> GetMostValuableProductCatalog()
        {
            // Obtener los productos más valiosos y su imagen principal, filtrando por visibilidad y estado activo
            var listProducts = await _identityDbContext.Products
                .Include(p => p.ProductPhotos)
                .Where(p => p.IsVisible && p.StateId == 1) // Filtrar por IsVisible y StateId
                .OrderByDescending(p => p.Price) // Ordenar por precio de mayor a menor
                .Take(20) // Tomar los primeros 20 elementos
                .Select(p => new ProductCatalogViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    imageUrl = p.ProductPhotos.FirstOrDefault(photo => photo.IsMain).Url
                })
                .ToListAsync();

            // Si la lista de productos es null, retorna una lista vacía
            if (listProducts == null)
            {
                return new List<ProductCatalogViewModel>();
            }

            return listProducts;
        }


    }
}
