using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_Service_Test
    {
        [TestMethod]
        public void UserService_IsUser_OK()
        {
            User user = new User();
            user.Username = "Mike";
            user.Password = "1234";

            UserService service = new UserService();
            bool result = service.IsUser(user);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void UserService_IsUser_KO()
        {
            User user = new User();
            user.Username = "Mike";
            user.Password = "1234";

            UserService service = new UserService();
            bool result = service.IsUser(user);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public async void AdministratedTestService_GetAdministratedTests_OK()
        {
            var myCtx = new TestPlatformDBEntities();
            var myRepo = new AdministratedTestRepository(myCtx);
            int count = 0;

            using (myCtx.Database.BeginTransaction())
            {
                try
                {
                    var tests = await myRepo.GetAdministratedTests();
                    count = tests.Count;
                }
                catch
                {
                    throw new Exception();
                }
            }

            Assert.AreEqual(1, count);

        }
    }
}
