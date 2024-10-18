using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class AddCartUseCase<TDTO>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper<TDTO, Cart> _mapper;

        public AddCartUseCase(IRepository<Cart> cartRepository, IMapper<TDTO, Cart> mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO cartDTO)
        {
            var cart = _mapper.ToEntity(cartDTO);

            if (cart.CartItems.Count <= 0)
                throw new ValidationException("El carrito debe conterner Items.");

            await _cartRepository.AddAsync(cart);
        }

    }
}
