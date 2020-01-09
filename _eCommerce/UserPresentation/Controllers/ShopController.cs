using BLL;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserPresentation.Models;

namespace UserPresentation.Controllers
{
    public class ShopController : Controller
    {
        readonly BrandService brandService = new BrandService(new ADO_BrandRepository());
        ProductService productService = new ProductService(new ADO_ProductRepository());

        [HttpGet]
        public ActionResult Shop()
        {
            ShopModel brandModel = new ShopModel();
            brandModel.Brands = brandService.GetBrands();
            return View("Shop", brandModel); 
        }

        [HttpPost]
        public ActionResult BrandDetails(ShopModel shopModel)
        {
            shopModel.ProductsForBrand = brandService.GetProductsBrand(shopModel.IdBrand);
            shopModel.Brands = brandService.GetBrands();
            return View("Shop", shopModel);
        }

        [HttpPost]
        public ActionResult ProductDetails(ShopModel shopModel)
        {
            shopModel.Product = productService.Details(shopModel.IdProduct);
            shopModel.Brand = brandService.GetBrand(shopModel.IdBrand);
            shopModel.ProductsForBrand = brandService.GetProductsBrand(shopModel.IdBrand);
            shopModel.Brands = brandService.GetBrands();
            return View("Shop", shopModel);
        }
    }
}