using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class UpdateCartUseCase<TDTO>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper<TDTO, Cart> _mapper;

        public UpdateCartUseCase(IRepository<Cart> cartRepository, IMapper<TDTO, Cart> mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(int cartId, TDTO cartDTO)
        {
            var cart = _mapper.ToEntity(cartDTO);

            if (cart.CartItems.Count <= 0)
                throw new ValidationException("El carrito debe contener Items.");

            await _cartRepository.UpdateAsync(cartId, cart);
        }
    }
}
