using AplicationLayer;
using EnterpriseLayer;

namespace Presenters.SaleViewModel
{
    public class CartResumePresenter : ICartResumePresenter<CartResumeViewModel>
    {
        public CartResumeViewModel Present(Cart cart, IEnumerable<Product> products)
        {
            if (cart.CartItems == null)
            {
                throw new ArgumentNullException(nameof(cart.CartItems), "Los items del carrito no pueden ser null.");
            }

            var cartItems = cart.CartItems.Select(item =>
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId)
                              ?? throw new Exception($"Producto con ID {item.ProductId} no encontrado");

                // Verificar si ProductPhotos es null
                var productPhotos = product.ProductPhotos ?? new List<ProductPhoto>();

                // Obtener la URL de la foto principal
                var mainPhotoUrl = productPhotos.FirstOrDefault(photo => photo.IsMain)?.Url
                                   ?? string.Empty;  // Usa un valor predeterminado si no hay foto principal

                return new CartItem
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = product.Name,
                    ImageUrl = mainPhotoUrl,
                    ExistentQuantity = product.Existence,
                    Quantity = item.Quantity,
                    Price = product.Price
                };
            }).ToList();

            return new CartResumeViewModel
            {
                Id = cart.Id,
                CartItems = cartItems
            };
        }
    }
}
