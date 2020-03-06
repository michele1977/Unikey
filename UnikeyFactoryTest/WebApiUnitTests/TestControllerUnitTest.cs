using System;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.WebAPI;
using System.Web.Http.Results;
using UnikeyFactoryTest.WebAPI.Controllers;
using UnikeyFactoryTest.Service;
using UnikeyFactoryTest.IService;

namespace WebApiUnitTests
{
    [TestClass]
    public class TestControllerUnitTest
    {
        private StandardKernel kernel;
        private ITestService service;

        [TestMethod]
        public async void TestController_Get_Ok()
        {
            var result = await service.GetTestById(60);

            Assert.AreEqual(60, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async void TestController_Get_Ko()
        {
            var result = await service.GetTestById(0);

        }

        [TestMethod]
        public async void TestController_GetByUrl_Ok()
        {
            var test = await service.GetTestById(60);

            var result = await service.GetTestByURL(test.URL);

            Assert.AreEqual(60, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async void TestController_GetByUrl_Ko()
        {
            var result = await service.GetTestByURL("InvalidUrl");
        }

        [TestMethod]
        public async void TestController_GetByFilter_Ok()
        {
            var filter = "UniqueTestJustForFun";

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var testToAdd = new TestBusiness()
                {
                    Title = filter,
                    UserId = 6
                };

                service.AddNewTest(testToAdd);
                var testRetrieved = await service.GetTestsByFilter(testToAdd.Title);

                Assert.AreEqual(1, testRetrieved.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async void TestController_GetByFilter_Ko()
        {
            var result = await service.GetTestsByFilter(null);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async void TestController_GetAll_Ok()
        {
            var returned = await service.GetTests();

            Assert.IsNotNull(returned);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async void TestController_Delete_Ok()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var testToAdd = new TestBusiness()
                {
                    Title = "Random",
                    UserId = 6
                };

                service.AddNewTest(testToAdd);
                await service.DeleteTest(testToAdd.Id);
                await service.GetTestById(testToAdd.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestController_Delete_Ko()
        {
            service.DeleteTest(-10);
        }



        [TestInitialize]
        public void Configure()
        {
            kernel = new StandardKernel();
            MapConfig.RegisterMap(kernel);
            service = kernel.Get<ITestService>();
        }
    }
}
