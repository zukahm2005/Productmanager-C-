using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product_Menegistion
{
    public interface IProductRepository
    {
        void Create(Product product);
        Product Read(int id);
        void Update(Product product);
        void Delete(int id);
        List<Product> GetAll();
    }
}