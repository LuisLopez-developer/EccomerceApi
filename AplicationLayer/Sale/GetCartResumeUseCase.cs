using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class GetCartResumeUseCase<TOutput>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly ICartResumePresenter<TOutput> _cartResumePresenter;
        private readonly IProductRepository<Product> _productRepository2;

        public GetCartResumeUseCase(
            ICartRepository cartRepository,
            IRepository<Product> productRepository,
            ICartResumePresenter<TOutput> _presenter,
            IProductRepository<Product> productRepository2
            )
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _cartResumePresenter = _presenter;
            _productRepository2 = productRepository2;
        }

        public async Task<TOutput> ExecuteAsync(string UserId)
        {
            var cart = await _cartRepository.GetByUserIdAsync(UserId) ?? throw new Exception("Carrito no encontrado");
            
            var productIds = cart.CartItems.Select(cd => cd.ProductId).ToList();
            var products = await _productRepository2.GetByIdsAsync(productIds);
        
            return _cartResumePresenter.Present(cart, products);
        }
    }
}
