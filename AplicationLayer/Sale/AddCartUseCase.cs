using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class AddCartUseCase<TDTO>
    {
        private readonly IMapper<TDTO, Cart> _mapper;
        private readonly ICartRepository _cartRepository;

        public AddCartUseCase(ICartRepository cartRepository, IMapper<TDTO, Cart> mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO cartDTO)
        {
            var cart = _mapper.ToEntity(cartDTO);

            if (cart.CartItems.Count <= 0)
                throw new ValidationException("El carrito debe conterner Items.");

            // verificar si el usuario ya tiene un carrito, si ya tiene solo actualizas, y si
            // no tiene, creas uno nuevo. Pero si el item contiene un producto que ya esta en el carrito
            // solo actualizas la cantidad del producto.
            if (await _cartRepository.UserHasCartAsync(cart.UserId))
            {
                var existingCart = await _cartRepository.GetByUserIdAsync(cart.UserId);
                foreach (var item in cart.CartItems)
                {
                    var existingItem = existingCart.CartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                    if (existingItem != null)
                    {
                        // Crear un nuevo CartItem con la cantidad actualizada
                        var updatedItem = new CartItem(existingItem.Id, existingItem.ProductId, existingItem.Quantity + item.Quantity, existingItem.CreatedAt);
                        existingCart.CartItems.Remove(existingItem);
                        existingCart.CartItems.Add(updatedItem);
                    }
                    else
                    {
                        existingCart.CartItems.Add(item);
                    }
                }
                await _cartRepository.UpdateAsync(existingCart.Id, existingCart);
                return;
            }

            await _cartRepository.AddAsync(cart);
        }

    }
}
