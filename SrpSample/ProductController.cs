using System;
using System.Collections.Generic;

namespace SrpSample
{
    public class ProductController: Controller
    {
        public ViewResult GetProducts(string productName)
        {
            var productNames = string.IsNullOrWhiteSpace(productName)
                ? new string[0]
                : productName.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            return View(new List<string>());
        }
    }
}
