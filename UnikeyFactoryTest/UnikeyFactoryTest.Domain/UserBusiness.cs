using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.UI;

namespace UnikeyFactoryTest.Domain
{
    public class UserBusiness : IUser<int>
    {
        public UserBusiness()
        {
            this.Tests = new List<TestBusiness>();
        }
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; }
        public List<TestBusiness> Tests { get; set; }
    }
}