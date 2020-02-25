using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;
using UserTest.AutoMappers;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_DeleteQuestion_UnitTest
    {
        private TestPlatformDBEntities GetContext()
        {
            return new TestPlatformDBEntities();
        }
        [TestMethod]
        public async Task DeleteQuestion_Ok()
        {
            TestPlatformDBEntities ctx = GetContext();
            var kernel = KernelBuilder.Build();
            var service = kernel.Get<ITestService>();

            Question resQuestion = new Question();

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Question question = new Question()
                {
                    TestId = 61,
                    Text = "TestQuestionForUnitTest",
                    Position = 0,
                    Answers = new List<Answer>(),
                    Test = ctx.Tests.SingleOrDefault(t => t.Id == 61)
                };

                ctx.Questions.Add(question);
                ctx.SaveChanges();

                await service.DeleteQuestionByIdFromTest(question.Id);

                ctx.Entry(question).State = EntityState.Detached;

                resQuestion = ctx.Questions.Find(question.Id);

                ctx.Dispose();
            }
            Assert.IsNull(resQuestion);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task DeleteQuestion_Ko()
        {
            var kernel = KernelBuilder.Build();
            var service = kernel.Get<ITestService>();

            await service.DeleteQuestionByIdFromTest(0);
        }

    }
}
