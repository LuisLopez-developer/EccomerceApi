namespace AplicationLayer.Sale
{
    public class GetCartUseCase<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _cartRepository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetCartUseCase(IRepository<TEntity> cartRepository, IPresenter<TEntity, TOutput> presenter)
        {
            _cartRepository = cartRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var carts = await _cartRepository.GetAllAsync();
            return _presenter.Present(carts);
        }
    }
}