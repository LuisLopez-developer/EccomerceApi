using System.Linq.Expressions;

namespace AplicationLayer.Sale
{
    public class GetEntitiesSearchUseCase<TModel, TEntity, TOutput>
    {
        private readonly IRepositorySearch<TModel, TEntity> _repository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetEntitiesSearchUseCase(IRepositorySearch<TModel, TEntity> repository, IPresenter<TEntity, TOutput> presenter)
        {
            _repository = repository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
        {
            var result = await _repository.GetAsync(predicate);

            return _presenter.Present(result);
        }
    }
}
