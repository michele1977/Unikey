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
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest
    {
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

                    TestService service = new TestService(new TestRepository());

                    service.AddNewTest(testToAdd);

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

            TestService service = new TestService(new TestRepository());

            List<TestBusiness> tests = await service.GetTestsByFilter(testName);

            Assert.AreEqual(0, tests.Count);
        }

    }
}
