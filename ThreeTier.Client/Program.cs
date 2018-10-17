using ThreeTier.BLL;
using ThreeTier.IBLL;
using ThreeTier.Model;

namespace ThreeTier.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IService<Product> service = new ProductService();
            service.Add(new Product());
        }
    }
}
