namespace AplicationLayer.GenericUseCases
{
    /* 
     * Este caso de uso genérico se utiliza para todas las entidades.
     * No se necesita nada adicional, como comportamientos distintos,
     * solo para recuperar todas las entidades de la entidad entrante y su salida.
     * Si se requiere algo adicional, se debe crear otro caso de uso más personalizado.
     */
    public class GetAllEntitiesUseCase<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetAllEntitiesUseCase(IRepository<TEntity> repository, IPresenter<TEntity, TOutput> presenter)
        {
            _repository = repository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
            => _presenter.Present(await _repository.GetAllAsync());

    }
}
