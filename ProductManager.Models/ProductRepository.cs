using System.Collections.Generic;
using System.Linq;

namespace ProductManager.Models
{
    public class ProductRepository
    {
        private static List<Product> products;

        static ProductRepository()
        {
            products = new List<Product>
            {
                new Product{ Id = 1, Name = "Pride and Prejudice", ProductType = ProductType.Book},
                new Product{Id = 2, Name = "Shoes", ProductType = ProductType.Clothing},
                new Product{ Id = 3, Name = "Remote control aircraft", ProductType = ProductType.Toy}
            };
        }

        public List<Product> GetProducts(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return products;
            return products.Where(p => p.Name == name).ToList();
        }
    }
}
