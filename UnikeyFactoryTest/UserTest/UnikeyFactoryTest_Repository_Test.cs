using System;
using System.Collections.Generic;
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

        [TestMethod]
        public void AdministratedTestRepository_UpdateSave_OK()
        {
            var myAdTest = new AdministratedTestBusiness
            {
                Id = 0,
                URL = "",
                TestSubject = "",
                TestId = 0,
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


            var myCtx = new TestPlatformDBEntities();
            var myRepo = new AdministratedTestRepository(myCtx);
            int g = 0;

            using (myCtx.Database.BeginTransaction())
            {
                try
                {
                    myRepo.Update_Save(myAdTest);
                    g = 1;
                }
                catch
                {
                    throw new Exception();
                }
            }

            Assert.AreEqual(1, g);
        }
    }
}
