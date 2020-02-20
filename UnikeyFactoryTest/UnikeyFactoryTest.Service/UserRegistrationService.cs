using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;

namespace UnikeyFactoryTest.Service
{
    public class UserRegistrationService : UserManager<UserBusiness, int>, IUserRegistrationService
    {
        public UserRegistrationService(IUserStore<UserBusiness, int> store) : base(store)
        {
        }
    }
}
