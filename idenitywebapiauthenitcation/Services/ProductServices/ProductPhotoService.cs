using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Services.Product
{
    public class ProductPhotoService : IProductPhoto
    {
        private readonly IdentityDbContext _identityDbContext;

        public ProductPhotoService(IdentityDbContext identityDbContext) 
        {
            _identityDbContext = identityDbContext;   
        }

        public async Task<ProductPhotoViewModel> CreateAsync(int productId, ProductPhotoViewModel productPhoto)
        {
            var newProductPhoto = new ProductPhoto 
            {
                ProductId = productId,
                Url = productPhoto.Url,
            };

            // Agregar la nueva foto de producto a la base de datos
            _identityDbContext.ProductPhotos.Add(newProductPhoto);
            await _identityDbContext.SaveChangesAsync(); // Aquí se asigna el ID de la nueva foto de producto

            // Devolver el objeto ProductPhotoViewModel recién creado con el ID asignado
            productPhoto.Id = newProductPhoto.Id;
            return productPhoto;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductPhotoViewModel>> SearchByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, ProductPhotoViewModel productPhoto)
        {
            throw new NotImplementedException();
        }
    }
}
