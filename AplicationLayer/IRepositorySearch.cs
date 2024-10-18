using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLayer
{
    public interface IRepositorySearch<TModel, T>
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<TModel, bool>> predicate);
    }
}
