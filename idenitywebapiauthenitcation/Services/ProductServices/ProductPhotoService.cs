using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services.Product
{
    public class ProductPhotoService : IProductPhoto
    {
        private readonly IdentityDbContext _identityDbContext;

        public ProductPhotoService(IdentityDbContext identityDbContext) 
        {
            _identityDbContext = identityDbContext;   
        }

        public async Task<List<ProductPhotoViewModel>> CreateAsync(int productId, List<ProductPhotoViewModel> productPhotos)
        {
            // Validar que solo una imagen sea la principal
            if (productPhotos.Count(p => p.IsMain) > 1)
            {
                throw new ArgumentException("Solo una imagen puede ser la principal (IsMain).");
            }

            // Obtener el número actual de imágenes para el producto
            var currentPhotoCount = await _identityDbContext.ProductPhotos
                .Where(p => p.ProductId == productId)
                .CountAsync();

            // Verificar que el número total de imágenes no exceda el máximo permitido
            if (currentPhotoCount + productPhotos.Count > 5)
            {
                throw new InvalidOperationException("El producto no puede tener más de 5 imágenes.");
            }

            // Verificar si ya existe una imagen principal para el producto
            var existingMainPhoto = await _identityDbContext.ProductPhotos
                .Where(p => p.ProductId == productId && p.IsMain)
                .FirstOrDefaultAsync();

            if (existingMainPhoto != null)
            {
                // Si hay una nueva imagen principal, desmarcar la actual
                var newMainPhoto = productPhotos.FirstOrDefault(p => p.IsMain);
                if (newMainPhoto != null)
                {
                    existingMainPhoto.IsMain = false;
                    _identityDbContext.ProductPhotos.Update(existingMainPhoto);
                }
            }

            // Crear nuevas entidades ProductPhoto
            var newProductPhotos = productPhotos.Select(photo => new ProductPhoto
            {
                ProductId = productId,
                Url = photo.Url,
                IsMain = photo.IsMain
            }).ToList();

            // Agregar las nuevas fotos de producto a la base de datos
            _identityDbContext.ProductPhotos.AddRange(newProductPhotos);
            await _identityDbContext.SaveChangesAsync();

            // Asignar los IDs generados a los ProductPhotoViewModels
            for (int i = 0; i < newProductPhotos.Count; i++)
            {
                productPhotos[i].Id = newProductPhotos[i].Id;
            }

            return productPhotos;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productPhoto = await _identityDbContext.ProductPhotos.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (productPhoto != null)
            {
                _identityDbContext.ProductPhotos.Remove(productPhoto);
                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<ProductPhotoViewModel>> SearchByProductIdAsync(int productId)
        {
            var productPhotos = await _identityDbContext.ProductPhotos
                .Where(p => p.ProductId == productId)
                .Select(photo => new ProductPhotoViewModel
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    IsMain = photo.IsMain,
                    ProductId = photo.ProductId
                })
                .ToListAsync();

            return productPhotos;
        }

        public async Task<bool> UpdateAsync(int id, ProductPhotoViewModel productPhoto)
        {
            var existingProductPhoto = await _identityDbContext.ProductPhotos.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (existingProductPhoto != null)
            {
                // Si la foto que se está actualizando se va a marcar como principal
                if (productPhoto.IsMain)
                {
                    // Verificar si ya existe una imagen principal para el producto
                    var existingMainPhoto = await _identityDbContext.ProductPhotos
                        .Where(p => p.ProductId == productPhoto.ProductId && p.IsMain && p.Id != id)
                        .FirstOrDefaultAsync();

                    if (existingMainPhoto != null)
                    {
                        // Desmarcar la actual imagen principal
                        existingMainPhoto.IsMain = false;
                        _identityDbContext.ProductPhotos.Update(existingMainPhoto);
                    }
                }

                existingProductPhoto.Url = productPhoto.Url;
                existingProductPhoto.IsMain = productPhoto.IsMain;
                existingProductPhoto.ProductId = productPhoto.ProductId;

                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
