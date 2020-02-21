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
        public void UserRegistrationRepository_Injection_OK()
        {
            //var userRepository = Kernel.Get<IUserRepository>();
            //Assert.ThrowsException<NotImplementedException>(() => userRepository.CreateAsync(new UserBusiness()));

            var userService = Kernel.Get<UserManager<UserBusiness,int>>();

        }

        [TestMethod]
        public async Task UserService_CreateAsync_OK()
        {
            //var service = Kernel.Get<UserManager<UserBusiness, int>>();
            var ctx = new TestPlatformDBEntities();
            var store = new UserRepository(ctx, Kernel);
            var service = new UserService(store);
            var user = new UserBusiness()
            {
                UserName = "Marione",
                Password = "Unikey1!"
            };

            var porcoddio = service.CreateAsync(user);
            int g = 0;
        }
    }
}
