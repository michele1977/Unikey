using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        public List<Product> GetProductsList();
        public void Save(int id, Product product);
        public void Delete(int id);
        public Product Details(int id);
        public void Update(Product product);
    }
}
