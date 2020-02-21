using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;

namespace UnikeyFactoryTest.Repository
{
    public class UserRepository :
        IUserRepository
    {
        public TestPlatformDBEntities _ctx { get; set; }
        public IMapper Mapper { get; set; }
        public IKernel Kernel { get; set; }

        public UserRepository(TestPlatformDBEntities ctx, IKernel kernel)
        {
            _ctx = ctx;
            Kernel = kernel;
            Mapper = kernel.Get<IMapper>("Light");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #region CRUD
        public async Task CreateAsync(UserBusiness userBusiness)
        {
            var user = Mapper.Map<User>(userBusiness);
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }

        public Task UpdateAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task<UserBusiness> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        //Da rendere async
        public async Task<UserBusiness> FindByNameAsync(string userName)
        {
            var result = Mapper.Map<UserBusiness>(
                await _ctx.Users.FirstOrDefaultAsync(u => u.Username.Equals(userName)));

            return result;
        }
        #endregion
        #region Password
        public Task SetPasswordHashAsync(UserBusiness user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Lockout
        public Task<DateTimeOffset> GetLockoutEndDateAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(UserBusiness user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(UserBusiness user, bool enabled)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Roles
        public Task AddToRoleAsync(UserBusiness user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserBusiness user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(UserBusiness user, string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
