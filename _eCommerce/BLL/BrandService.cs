using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BrandService
    {
        readonly IBrandRepository brandRepository;

        public BrandService(IBrandRepository param)
        {
            brandRepository = param;
        }
        
        public Brand GetBrand(int id)
        {
            return brandRepository.GetBrand(id);
        }

        public List<Brand> GetBrands()
        {
            return brandRepository.GetBrands();
        }

        public List<Product> GetProductsBrand(int id)
        {
            return brandRepository.GetProductsForBrand(id);
        }
    }
}
