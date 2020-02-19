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

        public UserRepository(TestPlatformDBEntities ctx)
        {
            _context = ctx;
        }


        //GET
        public async Task<bool> FindUser(User user)
        {
            var myTask = Task.Run(() => _context.Users.ToList().Exists(x => x.Username == user.Username && x.Password == user.Password));
            return await myTask;
        }

        public async Task<int> GetUserIdByUsername(User user)
        {
            var myTask = Task.Run(() => _context.Users.First(u => u.Username.Equals(user.Username)).Id);
            return await myTask;
        }

        //POST
        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
