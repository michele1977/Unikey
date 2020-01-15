using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_Repository_Test
    {
        [TestMethod]
        public async Task UserRepository_FindUser_OK()
        {
            User user = new User();
            user.Username = "Mike";
            user.Password = "1234";

            UserRepository repo = new UserRepository();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task UserRepository_FindUser_KO()
        {
            User user = new User();
            user.Username = "Mike";
            user.Password = "1234";

            UserRepository repo = new UserRepository();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public async Task AdministratedTestRepository_Add_OK()
        {
            var myCtx = new TestPlatformDBEntities();
            var myRepo = new AdministratedTestRepository(myCtx);

            var adTest = new AdministratedTestBusiness
            {
                Id = 0,
                TestId = 0,
                TestSubject = "",
                URL = "",
                AdministratedQuestions = new List<AdministratedQuestionBusiness>
                {
                    new AdministratedQuestionBusiness
                    {
                        Id = 0,
                        Text = "",
                        AdministratedTestId = 0,
                        AdministratedAnswers = new List<AdministratedAnswerBusiness>
                        {
                            new AdministratedAnswerBusiness
                            {
                                Id = 0,
                                Text = "",
                                AdministratedQuestionId = 0
                            }
                        }
                    }
                }
            };

            using (myCtx.Database.BeginTransaction())
            {
                try
                {
                    await myRepo.Add(adTest);
                }
                catch
                {
                    throw new Exception();
                }
            }

            var test = myCtx.AdministratedTests.Local[myCtx.AdministratedTests.ToList().Count];
            Assert.IsNotNull(test);

        }

        [TestMethod]
        public async Task AdministratedTestRepository_UpdateSave_OK()
        {
            var myCtx = new TestPlatformDBEntities();
            var myRepo = new AdministratedTestRepository(myCtx);
            
            var myAdTest = myRepo.GetAdministratedTests().GetAwaiter().GetResult()[0];

            myAdTest.AdministratedQuestions.ToList()[0].AdministratedAnswers.ToList()[0].isSelected = true;

            using (myCtx.Database.BeginTransaction())
            {
                try
                {
                    await myRepo.Update_Save(myAdTest);
                }
                catch
                {
                    throw new Exception();
                }
            }

            var test = myCtx.AdministratedTests.ToList()[0].AdministratedQuestions.Where(q => q.Id == 16).ToList()[0].AdministratedAnswers.ToList()[0].isSelected;
            
            Assert.IsTrue((bool)test);
        }
    }
}
