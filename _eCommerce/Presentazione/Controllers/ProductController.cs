using BLL;
using Domain;
using Presentazione.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentazione.Controllers
{
    public class ProductController : Controller
    {
        readonly ProductService productService = new ProductService(new ADO_ProductRepository());
        readonly BrandService brandService = new BrandService(new ADO_BrandRepository());

        [HttpGet]
        public ActionResult ProductsList()
        {
            ProductsListModel productModel = new ProductsListModel();
            productModel.Brands = brandService.GetBrands();
            return View("ProductsList", productModel);
        }

        [HttpPost]
        public ActionResult BrandDetails(ProductsListModel productModel)
        {
            productModel.ProductsForBrand = brandService.GetProductsBrand(productModel.IdBrand);
            productModel.Brands = brandService.GetBrands();
            return View("ProductsList", productModel);
        }

        [HttpPost]
        public ActionResult ProductDetails(ProductsListModel productModel)
        {
            productModel.Product = productService.Details(productModel.IdProduct);
            productModel.Brand = brandService.GetBrand(productModel.IdBrand);
            productModel.ProductsForBrand = brandService.GetProductsBrand(productModel.IdBrand);
            productModel.Brands = brandService.GetBrands();
            return View("ProductsList", productModel);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductsListModel productModel)
        {
            productService.Update(productModel.Product);
            productModel.ProductsForBrand = brandService.GetProductsBrand(productModel.IdBrand);
            productModel.Brands = brandService.GetBrands();
            productModel.Product = null;

            return View("ProductsList", productModel);
        }

        [HttpPost]
        public ActionResult DeleteProduct(ProductsListModel productModel)
        {
            productService.Delete(productModel.IdProduct);
            productModel.ProductsForBrand = brandService.GetProductsBrand(productModel.IdBrand);
            productModel.Brands = brandService.GetBrands();
            ModelState.Clear();
            return View("ProductsList", productModel);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            ProductRegistrationModel productModel = new ProductRegistrationModel();
            productModel.Brands = brandService.GetBrands();
            foreach (var brand in productModel.Brands)
            {
                productModel.SelectListItems.Add(new SelectListItem { Text = brand.Description, Value = (brand.ID).ToString() });
            }
            ModelState.Clear();
            return View("ProductRegistration", productModel);
        }

        [HttpPost]
        public ActionResult Save(ProductRegistrationModel productModel)
        {
            Product product = new Product(productModel.Price, productModel.Weigth, productModel.Color, productModel.Description);
            productModel = new ProductRegistrationModel
            {
                Check = productService.Save(productModel.IdBrand, product)
            };
            productModel.Brands = brandService.GetBrands();
            foreach (var brand in productModel.Brands)
            {
                productModel.SelectListItems.Add(new SelectListItem { Text = brand.Description, Value = (brand.ID).ToString() });
            }
            ModelState.Clear();
            return RedirectToAction("ProductsList", productModel);
        }
    }
}