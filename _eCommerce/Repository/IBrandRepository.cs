using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBrandRepository
    {
        public Brand GetBrand(int id);
        public List<Brand> GetBrands();
        public List<Product> GetProductsForBrand(int id);
    }
}
