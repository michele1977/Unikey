using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Repository;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_Repository_Test
    {
        [TestMethod]
        public async void UserRepository_FindUser_OK()
        {
            User user = new User();
            user.Username = "Mike";
            user.Password = "1234";

            UserRepository repo = new UserRepository();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async void UserRepository_FindUser_KO()
        {
            User user = new User();
            user.Username = "Mike";
            user.Password = "1234";

            UserRepository repo = new UserRepository();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(false, result);
        }
    }
}
