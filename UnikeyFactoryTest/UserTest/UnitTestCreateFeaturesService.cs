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

            TestService testService = new TestService();

            try
            {
                question.Answers.Add(answer);
                test.Questions.Add(question);
                testService.AddNewTest(test);
            }
            catch(Exception ex)
            {
                int g = 0;
            }
        }
    }
}
