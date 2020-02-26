using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;
using UserTest.AutoMappers;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_AdministratedTestRepository
    {
        private TestPlatformDBEntities GetContext()
        {
            return new TestPlatformDBEntities();
        }
        [TestMethod]
        public async Task GetState_OK()
        {
            //Non è presente il metodo get state nell'interfaccia del repository, quindi non è possibile fare la dependency injection
            AdministratedTestRepository testRepository = new AdministratedTestRepository(GetContext(), KernelBuilder.Build());
            var x = await testRepository.GetState(2);
            Assert.AreEqual(1,  x);
        }

        [TestMethod]
        public async Task AdministratedTestRepository_Add_OK()
        {
            var kernel = KernelBuilder.Build();

            var exTestService = kernel.Get<IAdministratedTestService>();
            var exTestRepository = kernel.Get<IAdministratedTestRepository>();
            var testService = kernel.Get<ITestService>();
            var testRepository = kernel.Get<ITestRepository>();
            var test = await testService.GetTestById(58);

            var exTest = exTestService.AdministratedTest_Builder(test, "Unit Test");

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var myCtx = GetContext();
                    exTestRepository.Add(exTest);
                    var testAdded = myCtx.AdministratedTests.FirstOrDefault(t => t.TestSubject == "Unit Test");
                    Assert.AreEqual(exTest.URL, testAdded.URL);
                }
                catch
                {
                    throw new Exception();
                }
                finally
                {
                    transaction.Dispose();
                }

            }
        }

        [TestMethod]
        public async Task AdministratedTestRepository_DeleteQuestion_OK()
        {
            var _ctx = GetContext();
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var kernel = KernelBuilder.Build();
                    var repo = kernel.Get<ITestRepository>();

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
                    transaction.Dispose();
                }
            }
        }
    }
}
