using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
<<<<<<< HEAD
using UnikeyFactoryTest.IRepository;
=======
using UnikeyFactoryTest.Repository;
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac
using UnikeyFactoryTest.Service;

namespace UserTest
{
    
    [TestClass]
    public class UnikeyFactoryTest_DeleteQuestion_UnitTest
    {
        private ITestRepository TestRepository;
        [TestMethod]
        public async Task DeleteQuestion_Ok()
        {
            TestPlatformDBEntities ctx = new TestPlatformDBEntities();
<<<<<<< HEAD
            TestService service = new TestService(TestRepository);
=======
            TestService service = new TestService(new TestRepository(ctx));
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac

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
<<<<<<< HEAD
            TestService service = new TestService(TestRepository);
=======
            TestPlatformDBEntities ctx = new TestPlatformDBEntities();
            TestService service = new TestService(new TestRepository(ctx));
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac

            await service.DeleteQuestionByIdFromTest(0);
        }

    }
}
