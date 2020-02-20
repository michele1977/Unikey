using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_AdministratedTestRepository
    {
        private IAdministratedTestRepository repository;
        private ITestRepository TestRepository;
        [TestMethod]
        public async Task GetState_OK()
        {
            AdministratedTestRepository testRepository = new AdministratedTestRepository();
            var x = await testRepository.GetState(2);
            Assert.AreEqual(1,  x);
        }

        [TestMethod]
        public async Task AdministratedTestRepository_Add_OK()
        {
            var myCtx = new TestPlatformDBEntities();
            var myRepo = new AdministratedTestRepository(myCtx);
            var exTestService = new AdministratedTestService(repository);
            var testService = new TestService(TestRepository);

            var test = await testService.GetTestById(60);
            var exTest = exTestService.AdministratedTest_Builder(test, "andrea bomber");

            //var adTest = new AdministratedTestBusiness
            //{
            //    TestId = 1,
            //    TestSubject = "",
            //    URL = "",
            //    AdministratedQuestions = new List<AdministratedQuestionBusiness>
            //    {
            //        new AdministratedQuestionBusiness
            //        {
            //            Text = "",
            //            AdministratedTestId = 986,
            //            AdministratedAnswers = new List<AdministratedAnswerBusiness>
            //            {
            //                new AdministratedAnswerBusiness
            //                {
            //                    Text = "",
            //                    AdministratedQuestionId = 987
            //                }
            //            }
            //        }
            //    }
            //};

            using (var trans = myCtx.Database.BeginTransaction())
            {
                try
                {
                    await myRepo.Add(exTest);
                    var testAdded = myCtx.AdministratedTests.FirstOrDefault(t => t.TestSubject == "andrea bomber");
                    Assert.AreEqual(exTest.URL, testAdded.URL);
                }
                catch
                {
                    throw new Exception();
                }
                finally
                {
                    trans.Rollback();
                }

            }

            //var g = 0;
            //try
            //{
                

            //    if (test is null)
            //        throw new Exception();
            //}
            //catch
            //{
            //    g = 1;
            //}

            //Assert.AreEqual(0, g);
        }

        [TestMethod]
        public async Task AdministratedTestRepository_DeleteQuestion_OK()
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
                    
                    await repo.DeleteQuestionByIdFromTest(question.Id);
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
