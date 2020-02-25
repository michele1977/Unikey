using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Repository;
using UserTest.AutoMappers;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_Repository_Test
    {
        [TestMethod]
        public async Task UserRepository_FindUser_OK()
        {
            User user = new User();
            user.Username = "ugo";
            user.Password = "123";

            var kernel = KernelBuilder.Build();
            var repo = kernel.Get<IUserRepository>();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task UserRepository_FindUser_KO()
        {
            User user = new User();
            user.Username = "ugo";
            user.Password = "1234";

            var kernel = KernelBuilder.Build();
            var repo = kernel.Get<IUserRepository>();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestRepository_GetQuestionById_OK()
        {
            var kernel = KernelBuilder.Build();
            var repo = kernel.Get<ITestRepository>();

            var question = repo.GetQuestionById(807);

            Assert.AreEqual("I vecchi cosa ammirano?", question.Result.Text);
        }

        [TestMethod]
        public async Task TestRepository_RicorsiveUpdateAndSave_OK()
        {
            var kernel = KernelBuilder.Build();
            var repo = kernel.Get<ITestRepository>();
            
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) 
            {
                try
                {
                    var oldTest = await repo.GetTest(58);
                    var testToUpdate = await repo.GetTest(58);
                    testToUpdate.Title = "Prova";
                    await repo.UpdateTest(testToUpdate);
                    var newTest = await repo.GetTest(58);

                    Assert.AreNotEqual(oldTest.Title, newTest.Title);
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
    }
}
