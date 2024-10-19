using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class GetOrderDetailByIdUseCase<TOutput>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IProductRepository<Product> _productRepository;
        private readonly IOrderDetailPresenter<TOutput> _presenter;

        public GetOrderDetailByIdUseCase(
            IRepository<Order> orderRepository,
            IProductRepository<Product> productRepository,
            IOrderDetailPresenter<TOutput> presenter)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _presenter = presenter;
        }

        public async Task<TOutput> ExecuteAsync(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Pedido no encontrado");
            }

            var productIds = order.OrderDetails.Select(od => od.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);

            return _presenter.Present(order, products);
        }

    }
}
