using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBizApplication.model;

namespace MyBizApplication.controller
{
    public class ProductController
    {
        private IProductRepository productService;

        public ProductController(IProductRepository productService)
        {
            this.productService = productService;
        }

        public void AddProduct(Product product){
            productService.AddProduct(product);
        }
        public List<Product> GetAllProducts(){
            return productService.GetAllProducts();
        }
    }
}