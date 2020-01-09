using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductService
    {
        readonly IProductRepository repository;

        public ProductService(IProductRepository param)
        {
            repository = param;
        }

        public List<Product> GetProductList()
        {
            return repository.GetProductsList();
        }

        public ErrorState Save(int id, Product product)
        {
            if(product.Description != null && product.Price >= 0)
            {
                repository.Save(id, product);
                return ErrorState.Yes;
            }
            else
            {
                return ErrorState.No;
            }
            
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Product Details(int id)
        {
            return repository.Details(id);
        }

        public void Update(Product product)
        {
            repository.Update(product);
        }
    }
}
