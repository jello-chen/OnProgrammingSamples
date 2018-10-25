using ProductManager.Models;
using ProductManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Presenters
{
    public class ProductPresenter : IPresenter
    {
        private readonly IProductView productView;
        private readonly ProductRepository productRepository;

        public ProductPresenter(IProductView productView)
        {
            this.productView = productView;
            this.productRepository = new ProductRepository();
            this.productView.ProductSelected += ProductView_ProductSelected;
        }

        private void ProductView_ProductSelected(object sender, ProductSelectedEventArgs e)
        {
            string productName = e.ProductName;
            List<Product> products = this.productRepository.GetProducts(productName);
            this.productView.BindProductList(products);
        }

        public void Initialize()
        {
            string[] productNames = new string[] { "Pride and Prejudice", "Shoes", "Remote control aircraft" };
            this.productView.BindProductName(productNames);
        }
    }
}
