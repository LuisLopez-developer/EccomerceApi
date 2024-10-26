namespace AplicationLayer.Sale
{
    public class GetTotalProductQuantityByUserIdUseCase
    {
        private readonly ICartRepository _cartRepository;

        public GetTotalProductQuantityByUserIdUseCase(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<int> ExecuteAsync(string userId)
        {
            return await _cartRepository.GetTotalProductQuantityByUserIdAsync(userId);
        }
    }
}
