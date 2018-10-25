using ProductManager.Models;
using System;
using System.Collections.Generic;

namespace ProductManager.Views
{
    public interface IProductView
    {
        void BindProductName(IEnumerable<string> names);
        void BindProductList(IEnumerable<Product> products);
        event EventHandler<ProductSelectedEventArgs> ProductSelected;
    }

    public class ProductSelectedEventArgs: EventArgs
    {
        public string ProductName { get; set; }
    }
}
