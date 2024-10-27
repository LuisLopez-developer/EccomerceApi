using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class GetCartItemsPaymentUseCase<TOutput>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository<Product> _productRepository;
        private readonly ICartResumePresenter<TOutput> _cartItemsPaymentPresenter;

        public GetCartItemsPaymentUseCase(
            ICartRepository cartRepository,
            IProductRepository<Product> productRepository,
            ICartResumePresenter<TOutput> cartItemsPaymentPresenter
            )
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _cartItemsPaymentPresenter = cartItemsPaymentPresenter;
        }

        public async Task<TOutput> ExecuteAsync(string UserId)
        {
            var cart = await _cartRepository.GetByUserIdAsync(UserId) ?? throw new Exception("Carrito no encontrado");

            var productIds = cart.CartItems.Select(cd => cd.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);

            return _cartItemsPaymentPresenter.Present(cart, products);
        }

    }
}
