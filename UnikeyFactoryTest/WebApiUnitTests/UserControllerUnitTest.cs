using System;
using System.Net;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.WebAPI;
using System.Web.Http.Results;

namespace WebApiUnitTests
{
    [TestClass]
    public class UserControllerUnitTest
    {
        private StandardKernel kernel;

        [TestMethod]
        public async void UserController_Subscribe_OK()
        {
            var userController = new UserController(kernel, LogManager.GetCurrentClassLogger());

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                UserBusiness user = new UserBusiness()
                {
                    UserName = "TestUser",
                    Password = "Valid_password_123"
                };

                var res = await userController.Subscribe(user);

                transaction.Dispose();

                Assert.AreEqual(200, res);
            }
        }

        [TestMethod]
        public async void UserController_Subscribe_KO()
        {
            var userController = new UserController(kernel, LogManager.GetCurrentClassLogger());

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                UserBusiness user = new UserBusiness()
                {
                    UserName = "TestUser",
                    Password = "123"
                };

                var res = await userController.Subscribe(user);

                transaction.Dispose();

                Assert.AreEqual(HttpStatusCode.BadRequest, res);
            }
        }

        [TestInitialize]
        public void Configure()
        {
            kernel = new StandardKernel();

            MapConfig.RegisterMap(kernel);
        }
    }
}
