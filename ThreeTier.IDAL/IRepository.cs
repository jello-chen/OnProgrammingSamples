using System;
using System.Linq;
using System.Linq.Expressions;

namespace ThreeTier.IDAL
{
    public interface IRepository<T>
    {
        T Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindList(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
