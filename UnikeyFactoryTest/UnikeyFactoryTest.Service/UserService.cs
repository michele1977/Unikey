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
    public class UserService : UserManager<UserBusiness, int>
    {
        public UserService(IUserStore<UserBusiness, int> store) : base(store)
        {
        }

        public override async Task<IdentityResult> CreateAsync(UserBusiness user)
        {
            try
            {
                await Store.CreateAsync(user);
                return new IdentityResult();
            }
            catch (Exception exc)
            {
                return new IdentityResult(exc.Message);
            }
            
            
        }

    }
}
