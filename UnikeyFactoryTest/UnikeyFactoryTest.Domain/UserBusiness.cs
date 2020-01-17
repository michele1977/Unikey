using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class UserBusiness
    {
        public UserBusiness()
        {
            this.Tests = new List<TestBusiness>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<TestBusiness> Tests { get; set; }
    }
}