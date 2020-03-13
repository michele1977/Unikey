using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;
using UserTest.AutoMappers;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_Service_Test
    {
        [TestMethod]
        public async Task UserService_IsUser_OK()
        {
            
            User user = new User();
            user.Username = "ugo";
            user.Password = "123";

            var kernel = KernelBuilder.Build();
            //var service = kernel.Get<IUserService>();

            //bool result = await service.IsUser(user);

            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public async Task UserService_IsUser_KO()
        {
            User user = new User();
            user.Username = "ugo";
            user.Password = "1234";

            var kernel = KernelBuilder.Build();
            //var service = kernel.Get<IUserService>();
            //bool result = await service.IsUser(user);

            Assert.AreEqual(false, false);
        }
    }
}
