using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using ClassLibrary1.Business;
using ClassLibrary1.Business.Entities;
using ClassLibrary1.DAL;
using ClassLibrary1.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using ClassLibrary1.DAL.Mapper;
using ClassLibrary1.DAL.DAO;

namespace Assestment.DAL.Test
{
    [TestClass]
    public class UnitTest1
    {
        //static ClassLibrary1.Business.Entities.Test GenerateTest(int numQuestions)
        //{
        //    var returned =  new ClassLibrary1.Business.Entities.Test
        //    {
        //        CreationDate = DateTime.Today,
        //        State = 0,
        //        Title = "Test to remove",
        //        Questions = new List<Question>()
        //    };
        //    for(var i=0;i<numQuestions;i++) returned.Questions.Add(GenerateQuestion());
        //    return returned;
        //}
        //static Question GenerateQuestion()
        //{
        //    var actualGuid = new Random().Next(0,5000);
        //    return new Question()
        //        {
        //            Type = 4,
        //            QuestionText = $"Question text x.1 {actualGuid}",
        //            State = 1,
        //            PossibleAnswers = new List<PossibleAnswer>()
        //            {
        //                GeneratePossibleAnswer(actualGuid.ToString()),
        //                GeneratePossibleAnswer(actualGuid.ToString()),
        //                GeneratePossibleAnswer(actualGuid.ToString()),
        //                GeneratePossibleAnswer(actualGuid.ToString()),
        //            }
        //    };

        //}

        //static PossibleAnswer GeneratePossibleAnswer(string actualGuid)
        //{
        //    return new PossibleAnswer() {IsCorrect = 1, Text = $"Possible Answer x1.1 {actualGuid}"};
        //}
        
        //[TestMethod]
        //public void TestRepository_Create_OK()
        //{
        //    try
        //    {
        //        var testToAdd = GenerateTest(5);
        //        var repo = new TestRepository();
        //        repo.AddTest(ref testToAdd);
        //        var res = repo.GetTest((testToAdd.Id));
        //        Assert.AreEqual(res, testToAdd);
        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}
        //[TestMethod]
        //public void TestRepository_Update_OK()
        //{
        //    try
        //    {
        //        var testToAdd = GenerateTest(5);
        //        var repo = new TestRepository();
        //        repo.AddTest(ref testToAdd);
        //        testToAdd.CreationDate = DateTime.Today.AddDays(-10);
        //        testToAdd.Questions.Remove(testToAdd.Questions.ElementAt(1));
        //        testToAdd.Questions.ElementAt(1).PossibleAnswers
        //            .Remove(testToAdd.Questions.ElementAt(1).PossibleAnswers.ElementAt(2));
        //        testToAdd.Questions.Add(GenerateQuestion());
        //        repo.UpdateTest(ref testToAdd);
                
        //        var res = repo.GetTest((testToAdd.Id));
        //        Assert.AreEqual(res, testToAdd);
        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}

        //[TestMethod]
        //public void TestRepositoryEx_Create_OK()
        //{
        //    try
        //    {
        //        var testToAdd = GenerateTest(5);
        //        var repo = new TestRepository();
        //        repo.AddTest(ref testToAdd);
        //        var factory = new TestExFactory();
        //        var exTest = factory.Create(testToAdd,"test test");
        //        var repo1 = new TestExRepository();
        //        repo1.AddTestEx(ref exTest);
        //        var res = repo1.GetTestEx((exTest.Id));
        //        Assert.AreEqual(res, exTest);
        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}

        //[TestMethod]
        //public void TestRepositoryEx_Update_OK()
        //{
        //    try
        //    {
        //        var testToAdd = GenerateTest(2);
        //        var repo = new TestRepository();
        //        repo.AddTest(ref testToAdd);
        //        var factory = new TestExFactory();
        //        var exTest = factory.Create(testToAdd, "test test");
        //        var repo1 = new TestExRepository();
        //        repo1.AddTestEx(ref exTest);
        //        exTest.Name = "Mike the best";
        //        exTest.ExQuestions.RemoveAt(0);
        //        repo1.UpdateTestEx(ref exTest);
        //        var res = repo1.GetTestEx((exTest.Id));
        //        Assert.AreEqual(res, exTest);
        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}
        //[TestMethod]
        //public void TestRepository_UpdateWithExistingExTest_OK()
        //{
        //    try
        //    {
        //        var testToAdd = GenerateTest(2);
        //        var repo = new TestRepository();
        //        repo.AddTest(ref testToAdd);
        //        var factory = new TestExFactory();
        //        var repo1 = new TestExRepository();
        //        var exTest1 = factory.Create(testToAdd, "test test 1");
        //        var exTest2 = factory.Create(testToAdd, "test test 1");
        //        repo1.AddTestEx(ref exTest1);
        //        repo1.AddTestEx(ref exTest2);
        //        testToAdd.CreationDate = DateTime.Today.AddDays(-10);
        //        testToAdd.Questions.Remove(testToAdd.Questions.ElementAt(1));
        //        testToAdd.Questions.ElementAt(0).PossibleAnswers
        //            .Remove(testToAdd.Questions.ElementAt(0).PossibleAnswers.ElementAt(2));
        //        testToAdd.Questions.Add(GenerateQuestion());
        //        repo.UpdateTest(ref testToAdd);

        //        var res = repo.GetTest((testToAdd.Id));
        //        Assert.AreEqual(res, testToAdd);
        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}

        [TestMethod]
        public void AutoMapperTest_OK()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExTest_Question,ExQuestion>());
            //var mapper = config.CreateMapper();

            config.AssertConfigurationIsValid();
        }
    }
}
