using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class GenerateOrderThroughCartUseCase
    {

        private readonly IRepository<Product> _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IUserRepository _userRepository;

        public GenerateOrderThroughCartUseCase(

            IRepository<Product> productRepository,
            ICartRepository cartRepository,
            IRepository<Order> orderRepository,
            IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(int cartId, int paymentMethodId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);

            var orderDetails = new List<OrderDetail>();

            foreach (var item in cart.CartItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new ValidationException($"El producto con ID {item.ProductId} no existe.");

                if (product.Existence < item.Quantity)
                    throw new ValidationException($"No hay suficiente cantidad del producto con ID {item.ProductId}.");

                // Reduce la existencia del producto
                product.Existence -= item.Quantity;
                await _productRepository.UpdateAsync(product.Id, product);

                orderDetails.Add(new OrderDetail(product.Id, item.Quantity, product.Price));
            }

            var user = await _userRepository.GetUserById(cart.UserId);

            var order = new Order.Builder()
                                .SetCustomerDNI(user.People.DNI)
                                .SetCustomerEmail(user.Email)
                                .SetStatusId(4) // Procesado
                                .SetPaymentMethodId(paymentMethodId)
                                .SetOrderDetails(orderDetails)
                                .SetCreatedAt(DateTime.Now)
                                .SetCreatedByUserId(user.Id)
                                .Build();

            await _orderRepository.AddAsync(order);

        }
    }
}
