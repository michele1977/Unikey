using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class UserBusiness
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public IEnumerable<TestBusiness> Tests { get; set; }
    }
}