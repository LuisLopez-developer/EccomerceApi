using System.Linq.Expressions;

namespace AplicationLayer
{
    public interface IRepositorySearch<TModel, T>
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<TModel, bool>> predicate);
    }
}
