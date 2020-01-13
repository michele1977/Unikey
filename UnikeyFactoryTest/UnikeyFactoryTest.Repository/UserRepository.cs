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

        public async Task<bool> FindUser(User user)
        {
            var myTask = Task.Run(() => _context.Users.ToList().Exists(x => x.Username == user.Username && x.Password == user.Password));
            return await myTask;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> GetUserIdByUsername(User user)
        {
            var myTask = Task.Run(() => _context.Users.First(u => u.Username.Equals(user.Username)).Id);
            return await myTask;
        }
    }
}
