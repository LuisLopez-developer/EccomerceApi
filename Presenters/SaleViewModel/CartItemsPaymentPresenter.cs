using AplicationLayer;
using EnterpriseLayer;

namespace Presenters.SaleViewModel
{
    public class CartItemsPaymentPresenter : ICartResumePresenter<CartItemsPaymentViewModel>
    {
        public CartItemsPaymentViewModel Present(Cart cart, IEnumerable<Product> products)
        {
            return new CartItemsPaymentViewModel
            {
                Id = cart.Id,
                CartItems = cart.CartItems.Select(item =>
                {
                    var product = products.FirstOrDefault(p => p.Id == item.ProductId)
                                  ?? throw new Exception($"Producto con ID {item.ProductId} no encontrado");

                    // Verificar si ProductPhotos es null
                    var productPhotos = product.ProductPhotos ?? new List<ProductPhoto>();

                    // Obtener la URL de la foto principal
                    var mainPhotoUrl = productPhotos.FirstOrDefault(photo => photo.IsMain)?.Url
                                       ?? string.Empty;  // Usa un valor predeterminado si no hay foto principal

                    return new CartItemPaymentViewModel
                    {
                        ProductName = product.Name,
                        ImageUrl = mainPhotoUrl,
                        Quantity = item.Quantity
                    };
                }).ToList()
            };
        }
    }
}
