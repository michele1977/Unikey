using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;

namespace UnikeyFactoryTest.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly TestPlatformDBEntities _context;

        public UserRepository()
        {
            _context = new TestPlatformDBEntities();
        }

        public bool FindUser(User user)
        {
            return _context.Users.ToList().Exists(x => x.Username == user.Username && x.Password == user.Password);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int GetUserIdByUsername(User user)
        {
            return _context.Users.First(u => u.Username.Equals(user.Username)).Id;
        }
    }
}
