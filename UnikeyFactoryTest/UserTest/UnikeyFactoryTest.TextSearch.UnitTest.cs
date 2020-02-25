using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;
using UserTest.AutoMappers;

namespace UserTest
{

    [TestClass]
    public class UnikeyFactoryTest
    {
        private string testName = "RandomTitleToMakeThisTestRunOk";
        private TestPlatformDBEntities GetContext()
        {
            return new TestPlatformDBEntities();
        }
        private TestBusiness AssembleTest()
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
            //creo le domande
            for (int i = 0; i < 4; i++)
            {
                testToAdd.Questions.Add(new QuestionBusiness()
                {
                    Position = 0,
                    Text = "Domanda" + i
                });
            }
            //per ogni domanda creo 4 risposte
            foreach (var question in testToAdd.Questions)
            {
                question.Answers = new List<AnswerBusiness>();
                for (int i = 0; i < 4; i++)
                {
                    question.Answers.Add(new AnswerBusiness()
                    {
                        IsCorrect = AnswerState.NotCorrect,
                        Score = 0,
                        Text = "Risposta" + i
                    });
                }
            }
            return testToAdd;
        }
        [TestMethod]
        public async Task TextSearch_Ok()
        {
            string result = "";
            using (TestPlatformDBEntities ctx = GetContext())
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var testToAdd = AssembleTest();
                    var kernel = KernelBuilder.Build();
                    var service = kernel.Get<ITestService>();

                    service.AddNewTest(testToAdd);
                    List<TestBusiness> tests = await service.GetTestsByFilter(testName);

                    TestBusiness testToGet = tests.Where(t => t.Title.Contains(testName)).FirstOrDefault();

                    result = testToGet.Title;

                    Assert.AreEqual(testName, result);
                    transaction.Dispose();
                }

            }

        }
        [TestMethod]
        public async Task TextSearch_Ko()
        {
            string testName = "ThisTestTitleDoesNotExist";
            var kernel = KernelBuilder.Build();
            var service = kernel.Get<ITestService>();
            //TestService service = new TestService(new TestRepository(GetContext(), GetKernel()), GetKernel());

            List<TestBusiness> tests = await service.GetTestsByFilter(testName);

            Assert.AreEqual(0, tests.Count);
        }

    }
}

