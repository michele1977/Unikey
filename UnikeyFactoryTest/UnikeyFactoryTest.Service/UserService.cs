using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;

namespace UnikeyFactoryTest.Service
{
    public class UserService : UserManager<UserBusiness, int>
    {
        public UserService(IUserRepository store) : base(store)
        {
            this.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 8,
                RequireUppercase = true,
                RequireLowercase = true,
                RequireNonLetterOrDigit = true,
                RequireDigit = true
            };

            this.PasswordHasher = new PasswordHasher();
        }

        public override async Task<IdentityResult> CreateAsync(UserBusiness user)
        {
            try
            {
                var userBusiness = await Store.FindByNameAsync(user.UserName);
                
                if (userBusiness != null)
                    throw new Exception("The user already exists");
                
                if (!this.PasswordValidator.ValidateAsync(user.Password).Result.Succeeded)
                    throw new Exception("Not valid password");

                user.Password = this.PasswordHasher.HashPassword(user.Password);

                await Store.CreateAsync(user);
                return new IdentityResult();
            }
            catch (Exception exc)
            {
                return new IdentityResult(exc.Message);
            }
        }

        public override async Task<UserBusiness> FindByNameAsync(string userName)
        {
            return await Store.FindByNameAsync(userName);
        }

        public override async Task<IdentityResult> UpdateAsync(UserBusiness user)
        {
            try
            {
                user.Password = this.PasswordHasher.HashPassword(user.Password);
                await Store.UpdateAsync(user);
                return new IdentityResult();
            }
            catch (Exception ex)
            {
                return new IdentityResult(ex.Message);
            }
        }

        public override Task<UserBusiness> FindByIdAsync(int userId)
        {
            return Store.FindByIdAsync(userId);
        }

        public override Task<bool> CheckPasswordAsync(UserBusiness user, string password)
        {
            return Task.FromResult(PasswordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success);
        }
    }
}
