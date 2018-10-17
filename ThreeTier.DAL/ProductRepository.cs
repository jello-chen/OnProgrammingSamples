using System;
using System.Linq;
using System.Linq.Expressions;
using ThreeTier.IDAL;
using ThreeTier.Model;

namespace ThreeTier.DAL
{
    public class ProductRepository : IRepository<Product>
    {
        public Product Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Find(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> FindList(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
