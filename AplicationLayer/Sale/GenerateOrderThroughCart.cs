using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class GenerateOrderThroughCart
    {
        private readonly IProductRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;

        public GenerateOrderThroughCart(
            IProductRepository<Product> productRepository,
            IRepository<Cart> cartRepository,
            IRepository<Order> orderRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public async Task ExecuteAsync(int cartId)
        {
           
        }

    }
}
