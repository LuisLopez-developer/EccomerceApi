using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class DeleteCartUseCase
    {
        private readonly IRepository<Cart> _cartRepository;

        public DeleteCartUseCase(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task ExecuteAsync(int cartId)
        {
            // Verificamos si el carrito existe antes de intentar eliminarlo
            var cart = await _cartRepository.GetById(cartId);

            if (cart == null)
            {
                throw new Exception($"Carrito con ID {cartId} no encontrado.");
            }

            // Llamamos al repositorio para eliminar el carrito
            await _cartRepository.DeleteAsync(cartId);
        }
    }
}
