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
    public class BrandRepository : IBrandRepository
    {
        readonly Context context = new Context();

        public Brand GetBrand(int id)
        {
            return context.Brands.FirstOrDefault(b => b.ID.Equals(id));
        }

        public List<Brand> GetBrands()
        {
            return context.Brands.ToList();
        }

        public List<Product> GetProductsForBrand(int id)
        {
            var brand = context.Brands.Where(b => b.ID.Equals(id)).Include(p => p.Products).FirstOrDefault();
            return brand.Products;
        }
    }
}
