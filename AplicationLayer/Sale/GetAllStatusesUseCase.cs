namespace AplicationLayer.Sale
{
    public class GetAllStatusesUseCase<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _statusRepository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetAllStatusesUseCase(IRepository<TEntity> statusRepository, IPresenter<TEntity, TOutput> presenter)
        {
            _statusRepository = statusRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
            => _presenter.Present(await _statusRepository.GetAllAsync());
    }
}
