using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
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
    }
}
