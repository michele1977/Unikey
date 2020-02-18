using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnitTestCreateFeaturesService
    {
        [TestMethod]
        public void TestMethodSaveChangesDB_ERR()
        {
            TestBusiness test = new TestBusiness()
            {
                Date = DateTime.Now,
                UserId = 1
            };
            QuestionBusiness question = new QuestionBusiness()
            {
                TestId = 1,

            };
            AnswerBusiness answer = new AnswerBusiness()
            {
                QuestionId = 1,
                Score = 10,

            };

            //TestService testService = new TestService();

            question.Answers.Add(answer);
            test.Questions.Add(question);

            //var res = Task.Run(() => testService.AddNewTest(test));
        }
    }
}
