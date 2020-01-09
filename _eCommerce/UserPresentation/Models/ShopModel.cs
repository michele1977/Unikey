using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserPresentation.Models
{
    public class ShopModel
    {
        public List<Brand> Brands { get; set; }
        public List<Product> ProductsForBrand { get; set; }
        public Product Product { get; set; }
        public Brand Brand { get; set; }
        public int IdBrand { get; set; }
        public int IdProduct { get; set; }
    }
}