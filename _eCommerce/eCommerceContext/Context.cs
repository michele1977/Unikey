using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.DataModel
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
