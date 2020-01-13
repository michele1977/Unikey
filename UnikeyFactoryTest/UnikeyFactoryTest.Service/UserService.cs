using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class UserService
    {
        public async Task<bool> IsUser(User user)
        {
            using (UserRepository repo = new UserRepository())
            {
                return await repo.FindUser(user);
            }
        }

        public async Task<int> GetUserIdByUsername(User user)
        {
            using (UserRepository repo = new UserRepository())
            {
                return await repo.GetUserIdByUsername(user);
            }
        }

    }
}
