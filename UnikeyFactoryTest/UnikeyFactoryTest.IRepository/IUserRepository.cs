using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.IRepository
{
    public interface IUserRepository : 
        IUserStore<UserBusiness,int>,
        IUserPasswordStore<UserBusiness,int>,
        IUserLockoutStore<UserBusiness,int>,
        IUserRoleStore<UserBusiness,int>
    {
    }
}
