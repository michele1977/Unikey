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
                TestId = 1,
                TestSubject = "",
                URL = "",
                AdministratedQuestions = new List<AdministratedQuestionBusiness>
                    {
                        new AdministratedQuestionBusiness
                        {
                            Text = "",
                            AdministratedTestId = 986,
                            AdministratedAnswers = new List<AdministratedAnswerBusiness>
                            {
                                new AdministratedAnswerBusiness
                                {
                                    Text = "",
                                    AdministratedQuestionId = 987
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

            var g = 0;
            try
            {
                var test = myCtx.AdministratedTests.FirstOrDefault(t => t.Id == 994);

                if (test is null)
                    throw new Exception();
            }
            catch
            {
                g = 1;
            }

            Assert.AreEqual(0, g);
        }

        //    [TestMethod]
        //    public async Task AdministratedTestRepository_UpdateSave_OK()
        //    {
        //        var myCtx = new TestPlatformDBEntities();
        //        var myRepo = new UnikeyFactoryTest_AdministratedTestRepository(myCtx);

        //        var myAdTest = myRepo.GetAdministratedTests().GetAwaiter().GetResult()[0];

        //        myAdTest.AdministratedQuestions.ToList()[0].AdministratedAnswers.ToList()[0].isSelected = true;

        //        using (myCtx.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                await myRepo.Update_Save(AdministratedTestMapper.MapDaoToDomain(myAdTest));
        //            }
        //            catch
        //            {
        //                throw new Exception();
        //            }
        //        }

        //        var test = myCtx.AdministratedTests.ToList()[0].AdministratedQuestions.Where(q => q.Id == 16).ToList()[0].AdministratedAnswers.ToList()[0].isSelected;

        //        Assert.IsTrue((bool)test);
        //    }

        [TestMethod]
        public void AdministratedTestRepository_DeleteQuestion_OK()
        {
            var _ctx = new TestPlatformDBEntities();
            using (var trans =_ctx.Database.BeginTransaction())
            {
                try
                {
                    var repo = new TestRepository(_ctx);

                    var User = new User();
                    
                    User.Password = "1234";
                    User.Username = "Develollo";
                    _ctx.Users.Add(User);
                    _ctx.SaveChanges();

                    var Test = new Test();
                    Test.Date = DateTime.Now;
                    Test.Title = "ProvaTest";
                    Test.URL = "myURL";
                    Test.UserId = _ctx.Users.FirstOrDefault(u=> u.Username == "Develollo").Id;
                    _ctx.Tests.Add(Test);
                    _ctx.SaveChanges();

                    var question = new Question()
                    {
                        Text = "ProvaQuestion",
                        Position = 1,
                        TestId = Test.Id,
                    };

                    Test.Questions.Add(question);
                    _ctx.SaveChanges();


                    Test.Questions.FirstOrDefault().Answers.Add(new Answer()
                    {
                        Text = "ProvaAnswer",
                        IsCorrect = 1,
                        QuestionId = question.Id,
                        Score = 30
                    });

                    _ctx.SaveChanges();


                    var result = _ctx.Tests.FirstOrDefault(t => t.Id == Test.Id).Questions.Count;
                    
                    repo.DeleteQuestionByIdFromTest(question.Id);
                    _ctx.SaveChanges();

                    var expected = _ctx.Tests.FirstOrDefault(t => t.Id == Test.Id).Questions.Count;

                    Assert.AreEqual(expected, result - 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    trans.Rollback();
                }
            }
        }
    }
}
