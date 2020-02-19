using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class UserService : IUserService
    {
        private IUserRepository Repo;

        public UserService(IUserRepository value)
        {
            Repo = value;
        }

        public async Task<bool> IsUser(User user)
        {
            return await Repo.FindUser(user);
        }

        public async Task<int> GetUserIdByUsername(User user)
        {
            return await Repo.GetUserIdByUsername(user);
        }

        public void InsertUser(User user)
        {
            Repo.InsertUser(user);
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

    }
}
