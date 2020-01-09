using Domain;
using eCommerce.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository
    {
        readonly Context context = new Context();

        public List<Product> GetProductsList()
        {
            return context.Products.ToList();
        }

        public void Save(int id, Product product)
        {
            context.Brands.FirstOrDefault(b => b.ID.Equals(id)).Products.Add(product);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Products.Remove(context.Products.FirstOrDefault(p => p.ID == id));
            context.SaveChanges();
        }

        public Product Details(int id)
        {
            return context.Products.FirstOrDefault(m => m.ID == id);
        }

        public void Update(Product product)
        {
            var _product = product;
            context.Products.Attach(_product);
            context.Entry(_product).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
