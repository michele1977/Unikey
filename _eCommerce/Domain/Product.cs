using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public double Weigth { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }

        public Product() { }

        public Product(double price, double weigth, string color, string description)
        {
            Price = price;
            Weigth = weigth;
            Color = color;
            Description = description;
        }
    }
}
