using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBizApplication.model;

namespace MyBizApplication
{
    public interface IProductRepository
    {

        void AddProduct(Product product);
        List<Product> GetAllProducts();
        
    }
}