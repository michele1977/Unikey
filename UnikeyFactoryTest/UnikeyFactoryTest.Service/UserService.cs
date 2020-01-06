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
        public bool IsUser(User user)
        {
            using (UserRepository repo = new UserRepository())
            {
                return repo.FindUser(user);
            }
        }

    }
}
