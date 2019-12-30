using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTestDAL.Test
{
    [TestClass]
    public class UnitTestCreateFeaturesRepository
    {
        [TestMethod]
        public void TestMethodSaveChangesDB_OK()
        {
            UnikeyFactoryTest.Context.Test test = new UnikeyFactoryTest.Context.Test()
            {
                Date = DateTime.Now,
                URL = "Test URL",
                UserId = 1
            };

            Question question = new Question()
            {
                TestId = 1,
                Text = "Question Test"

            };
            Question question2 = new Question()
            {
                TestId = 1,
                Text = "Question Test"

            };
            Answer answer = new Answer()
            {
                IsCorrect = true,
                QuestionId = 1,
                Score = 10,
                Text = "Answer Test"
            };
            Answer answer2 = new Answer()
            {
                IsCorrect = true,
                QuestionId = 2,
                Score = 11,
                Text = "Answer Test2"
            };

            List<Answer> answers = new List<Answer>()
            {
                answer,
                answer2
            };

            AnswerRepository answerRepository = new AnswerRepository();
            QuestionRepository questionRepository = new QuestionRepository();
            TestRepository testRepository = new TestRepository();

            try
            {
                testRepository.SaveTest(test);
                questionRepository.SaveQuestion(question);
                questionRepository.SaveQuestion(question2);
                answerRepository.SaveAnswers(answers);
            }
            catch (Exception ex)
            {
                int gg = 0;
            }

            TestPlatformDBEntities _ctx = new TestPlatformDBEntities();
            Assert.AreEqual(1, _ctx.Tests.Count());
            Assert.AreEqual(2, _ctx.Questions.Count());
            Assert.AreEqual(2, _ctx.Answers.Count());
            int g = 0;
        }
    }
}
