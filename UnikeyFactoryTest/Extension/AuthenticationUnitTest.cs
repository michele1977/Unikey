using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Modules;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.NinjectConfiguration;

namespace Extension
{
    [TestClass]
    public class AuthenticationUnitTest
    {
        private IKernel Kernel { get; set; } = new StandardKernel(new List<INinjectModule>
        {
            new UnikeyFactoryTestBindings()
        }.ToArray());

        [TestMethod]
        public void UserRegistrationRepository_Injection_OK()
        {
            //var userRepository = Kernel.Get<IUserRepository>();
            //Assert.ThrowsException<NotImplementedException>(() => userRepository.CreateAsync(new UserBusiness()));

            var userService = Kernel.Get<UserManager<UserBusiness,int>>();

        }
    }
}
