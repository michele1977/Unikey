using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Modules;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.NinjectConfiguration;
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
                UserName = "Danielone",
                Password = "Unikey11!"
            };

            var result = await Task.Run(() => service.CreateAsync(user));
            int g = 0;
        }
    }
}
