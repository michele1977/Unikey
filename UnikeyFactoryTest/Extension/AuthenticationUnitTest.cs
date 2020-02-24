using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Razor.Tokenizer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Modules;
using Owin;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.Presentation;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace Extension
{
    [TestClass]
    public class AuthenticationUnitTest
    {
        private IKernel Kernel { get; set; } = new StandardKernel(new List<INinjectModule>
        {
            new UnikeyFactoryTestBindings(),
            new AutoMapperBindingsService()
        }.ToArray());

        [TestMethod]
        public async Task UserRegistrationRepository_Injection_OK()
        {
            //var userRepository = Kernel.Get<IUserRepository>();
            //Assert.ThrowsException<NotImplementedException>(() => userRepository.CreateAsync(new UserBusiness()));

            var userService = Kernel.Get<UserManager<UserBusiness,int>>();
            var marione = await Task.Run(() => userService.FindByNameAsync("Marione"));
            int g = 0;
        }

        [TestMethod]
        public async Task UserService_CreateAsync_OK()
        {
            var service = Kernel.Get<UserManager<UserBusiness, int>>();
            var user = new UserBusiness()
            {
                UserName = "Hendrichone",
                Password = "Unikey11!"
            };

            var result = await Task.Run(() => service.CreateAsync(user));
            int g = 0;
        }
        
        [TestMethod]
        public async Task UserRepo_UpdateAsync_OK()
        {
            var service = Kernel.Get<UserManager<UserBusiness, int>>();
            var user = new UserBusiness()
            {
                Id = 14,
                UserName = "Hendriccione",
                Password = "Unikey11!"
            };

            var result = await Task.Run(() => service.UpdateAsync(user));
            int g = 0;
        }
        
        [TestMethod]
        public void HashPassword_OK()
        {
            var service = Kernel.Get<UserManager<UserBusiness, int>>();
            var pw = service.PasswordHasher.HashPassword("Unikey01!");
            var res = service.PasswordHasher.VerifyHashedPassword("AEP+CotwBszffjMdSyl3/W1rgZa8X/9jG7MrVT9ucmqGHdGPZ0KT4wcnPZsRWH6jqQ==", "Unikey01!");
        }
    }
}
