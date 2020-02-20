using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
<<<<<<< HEAD
=======
using UnikeyFactoryTest.Repository;
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest
    {
        private ITestRepository TestRepository;
        [TestMethod]
        public async Task TextSearch_Ok()
        {
            string testName = "RandomTitleToMakeThisTestRunOk";

            string result = "";

            using (TestPlatformDBEntities ctx = new TestPlatformDBEntities())
            {
                using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                {
                    TestBusiness testToAdd = new TestBusiness()
                    {
                        AdministratedTests = new List<AdministratedTestBusiness>(),
                        Date = DateTime.Now,
                        Questions = new List<QuestionBusiness>(),
                        Title = testName,
                        URL = "randomUrl",
                        UserId = 6
                    };

<<<<<<< HEAD
                    TestService service = new TestService(TestRepository);
=======
                    TestService service = new TestService(new TestRepository());
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac

                    await service.AddNewTest(testToAdd);

                    List<TestBusiness> tests = await service.GetTestsByFilter(testName);

                    TestBusiness testToGet = tests.Where(t => t.Title.Contains(testName)).FirstOrDefault();

                    result = testToGet.Title;

                    transaction.Rollback();
                }
            }
            Assert.AreEqual(testName, result);
        }

        [TestMethod]
        public async Task TextSearch_Ko()
        {
            string testName = "ThisTestTitleDoesNotExist";

            string result = "";

<<<<<<< HEAD
            TestService service = new TestService(TestRepository);
=======
            TestService service = new TestService(new TestRepository());
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac

            List<TestBusiness> tests = await service.GetTestsByFilter(testName);

            Assert.AreEqual(0, tests.Count);
        }

    }
}
