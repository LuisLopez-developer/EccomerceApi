using EnterpriseLayer;
using System.Linq.Expressions;

namespace AplicationLayer.Sale
{
    public class GetCartSearchUseCase<TModel>
    {
        private readonly IRepositorySearch<TModel, Cart> _cartRepository;

        public GetCartSearchUseCase(IRepositorySearch<TModel, Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<Cart>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _cartRepository.GetAsync(predicate);
        }
    }
}
