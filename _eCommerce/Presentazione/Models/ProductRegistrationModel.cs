using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentazione.Models
{
    public class ProductRegistrationModel
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public double Weigth { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public List<Brand> Brands { get; set; }
        public List<SelectListItem> SelectListItems = new List<SelectListItem>();
        public int IdBrand { get; set; }
        public ErrorState Check = ErrorState.Empty;
    }
}