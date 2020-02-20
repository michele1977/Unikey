﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_Service_Test
    {
        private readonly IUserRepository services;
        [TestMethod]
        public async Task UserService_IsUser_OK()
        {
            User user = new User();
            user.Username = "ugo";
            user.Password = "123";

<<<<<<< HEAD
            UserService service = new UserService(services);
=======
            UserService service = new UserService(new UserRepository());
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac
            bool result = await service.IsUser(user);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task UserService_IsUser_KO()
        {
            User user = new User();
            user.Username = "ugo";
            user.Password = "1234";

<<<<<<< HEAD
            UserService service = new UserService(services);
=======
            UserService service = new UserService(new UserRepository());
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac
            bool result = await service.IsUser(user);

            Assert.AreEqual(false, result);
        }
    }
}
