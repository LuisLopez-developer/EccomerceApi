using EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLayer.Sale
{
    public class GetTotalAmountCartUseCase 
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository<Product> _productRepository;

        public GetTotalAmountCartUseCase(ICartRepository cartRepository, IProductRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<decimal> ExecuteAsync(string userId)
        {
            var cart = await _cartRepository.GetByUserIdAsync(userId) ?? throw new Exception("El carrito no existe.");

            // obtenr los productos id del carrito
            var productIds = cart.CartItems.Select(x => x.ProductId).ToList();

            // recuperar el precio de los productos
            var products = await _productRepository.GetByIdsAsync(productIds);

            // calcular el total
            decimal total = 0;
            foreach (var item in cart.CartItems)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId) ?? throw new Exception($"Producto con ID {item.ProductId} no encontrado.");
                total += product.Price * item.Quantity;
            }

            return total;

        }
    }
}
