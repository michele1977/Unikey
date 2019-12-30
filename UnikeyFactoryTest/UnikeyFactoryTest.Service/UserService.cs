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
        UserRepository repository = new UserRepository();

        public bool IsUser(User user)
        {
            return repository.FindUser(user);
        }
    }
}
