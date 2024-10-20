namespace AplicationLayer.Sale
{
    public class GetAllOrdersUseCase<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _orderRepository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetAllOrdersUseCase(IRepository<TEntity> orderRepository, IPresenter<TEntity, TOutput> presenter)
        {
            _orderRepository = orderRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
            => _presenter.Present(await _orderRepository.GetAllAsync());

    }
}
