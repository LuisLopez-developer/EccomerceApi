using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class GetCartUseCase
    {
        private readonly IRepository<Cart> _cartRepository;


        public GetCartUseCase(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<Cart>> ExecuteAsync()
        {
            return await _cartRepository.GetAllAsync();
        }
    }
}