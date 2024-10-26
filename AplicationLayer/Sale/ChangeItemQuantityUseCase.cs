using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class ChangeItemQuantityUseCase<TDTO>
    {
        private readonly IMapper<TDTO, CartItem> _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository<Product> _productRepository;

        public ChangeItemQuantityUseCase(IMapper<TDTO, CartItem> mapper, ICartRepository cartRepository, IProductRepository<Product> productRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(TDTO dto)
        {
            var cartItem = _mapper.ToEntity(dto);

            await _cartRepository.ChangeItemQuantityAsync(cartItem.Id, cartItem.Quantity);

        }

    }
}
