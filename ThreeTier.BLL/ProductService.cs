using System;
using ThreeTier.DAL;
using ThreeTier.IBLL;
using ThreeTier.IDAL;
using ThreeTier.Model;

namespace ThreeTier.BLL
{
    public class ProductService : IService<Product>
    {
        private readonly IRepository<Product> repository;

        public ProductService()
        {
            this.repository = new ProductRepository();
        }

        public Product Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
