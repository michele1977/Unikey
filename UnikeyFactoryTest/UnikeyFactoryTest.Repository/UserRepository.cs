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
            _ctx.Dispose();
        }

        #region CRUD
        public async Task CreateAsync(UserBusiness userBusiness)
        {
            var user = Mapper.Map<User>(userBusiness);
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserBusiness userBusiness)
        {
            var user = Mapper.Map<User>(userBusiness);
            var userDal = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == userBusiness.Id);
            
            if(userDal is null)
                throw new Exception("Invalid User Id");

            userDal.Username = user.Username;
            userDal.Password = user.Password;

            await _ctx.SaveChangesAsync();
        }

        public Task DeleteAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserBusiness> FindByIdAsync(int userId)
        {
           var user = await _ctx.Users.FindAsync(userId);
           var result = Mapper.Map<UserBusiness>(user);
           return result;
        }

        public async Task<UserBusiness> FindByNameAsync(string userName)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Username.Equals(userName));
            var result = Mapper.Map<UserBusiness>(user);
            return result;
        }
        #endregion
        #region Password
        public Task SetPasswordHashAsync(UserBusiness user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(UserBusiness user)
        {
            try
            {
                var userDal = await _ctx.Users.FindAsync(user.Id);
                return userDal.Password;
            }
            catch (Exception ex)
            {
            }

            return string.Empty;
        }

        public Task<bool> HasPasswordAsync(UserBusiness user)
        {
            throw new NotImplementedException();
        }
        #endregion

        // non implementato
        #region Lockout

        public Task<DateTimeOffset> GetLockoutEndDateAsync(UserBusiness user)
        {
            return null;
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
            return null;
        }

        public Task<int> GetAccessFailedCountAsync(UserBusiness user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(UserBusiness user)
        {
            return Task.FromResult(false);
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
            var list = new List<string>();
            return Task.Run<IList<string>>(() => list);
        }

        public Task<bool> IsInRoleAsync(UserBusiness user, string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region TwoFactor
        public Task SetTwoFactorEnabledAsync(UserBusiness user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(UserBusiness user)
        {
            return Task.FromResult(false);
        }
        #endregion
    }
}
