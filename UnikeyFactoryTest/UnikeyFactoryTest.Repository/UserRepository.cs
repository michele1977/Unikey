using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.Repository
{
    public class UserRepository
    {
        TestPlatformDBEntities _context = new TestPlatformDBEntities();
        public bool FindUser(User user)
        {
            return _context.Users.ToList().Exists(x => x.Username == user.Username && x.Password == user.Password);
        }
    }
}
