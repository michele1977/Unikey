using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTestBusiness.Test
{
    [TestClass]
    public class UnitTestCreateFeaturesService
    {
        [TestMethod]
        public void TestMethodSaveChangesDB_ERR()
        {
            UnikeyFactoryTest.Context.Test test = new UnikeyFactoryTest.Context.Test()
            {
                Date = DateTime.Now,
                UserId = 1
            };
            Question question = new Question()
            {
                TestId = 1,

            };
            Answer answer = new Answer()
            {
                IsCorrect = true,
                QuestionId = 1,
                Score = 10,

            };
            Answer answer2 = new Answer()
            {
                IsCorrect = true,
                QuestionId = 2,
                Score = 11,
                Text = "Answer Test"
            };

            List<Answer> answers = new List<Answer>()
            {
                answer,
                answer2
            };

            TestService testService = new TestService();
            QuestionService questionService = new QuestionService();
            AnswerService answerService = new AnswerService();

            try
            {
                testService.AddNewTest(TestMapper.MapDalToBiz(test));
            }
            catch(Exception ex)
            {
                int g = 0;
            }
            try
            {
                questionService.AddNewQuestions(question);
            }
            catch (Exception ex)
            {
                int g = 0;
            }
            try
            {
                answerService.AddNewAnswers(answers);
            }
            catch (Exception ex)
            {
                int g = 0;
            }
        }
    }
}
