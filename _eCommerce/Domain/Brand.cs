using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Brand
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
