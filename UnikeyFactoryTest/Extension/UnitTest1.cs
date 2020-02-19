using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.Repository;

namespace Extension
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var repo = new TestRepository();

            var mytest = new TestBusiness();

            mytest.Id = 60;
            mytest.Date = DateTime.Now;
            mytest.Title = "StupidTest";
            //mytest.Questions.Add(new QuestionBusiness()
            //{
            //    Text = "Dimmi perchè?",
            //    TestId = 60
            //});
            mytest.Questions.Add(new QuestionBusiness()
            {
                Id = 709,
                Text = "Dimmi come ti chiami?",
                TestId = 60,
                
            });
            mytest.Questions.Add(new QuestionBusiness()
            {
                Id = 710,
                Text = "Da quanti anni sei nato?",
                TestId = 60
            });
            mytest.Questions.FirstOrDefault(q=>q.Id == 710).Answers.Add(new AnswerBusiness()
            {
                Id = 2832,
                Text = "Da più dello scorso anno",
                Score = 100,
                QuestionId = 710,
                IsCorrect = AnswerState.Correct
            });
            mytest.Questions.FirstOrDefault(q => q.Id == 710).Answers.Add(new AnswerBusiness()
            {
                Id = 2833,
                Text = "Da meno di te",
                Score = 1,
                QuestionId = 710,
                IsCorrect = AnswerState.NotCorrect
            });
            mytest.Questions.FirstOrDefault(q => q.Id == 710).Answers.Add(new AnswerBusiness()
            {
                Id = 2834,
                Text = "Da più di mia madre",
                Score = 1,
                QuestionId = 710,
                IsCorrect = AnswerState.NotCorrect
            });
            mytest.Questions.FirstOrDefault(q => q.Id == 710).Answers.Add(new AnswerBusiness()
            {
                Text = "Fatti gli affari tuoi",
                Score = 80,
                QuestionId = 710,
                IsCorrect = AnswerState.NotCorrect
            });
            mytest.Questions.FirstOrDefault(q => q.Id == 709).Answers.Add(new AnswerBusiness()
            {
                Id = 2829,
                Text = "Mi chiamano gli altri",
                Score = 1,
                QuestionId = 709,
                IsCorrect = AnswerState.Correct
            });
            mytest.Questions.FirstOrDefault(q => q.Id == 709).Answers.Add(new AnswerBusiness()
            {
                Id = 2830,
                Text = "Con il mio nome",
                Score = 1,
                QuestionId = 709,
                IsCorrect = AnswerState.NotCorrect
            });
            mytest.Questions.FirstOrDefault(q => q.Id == 709).Answers.Add(new AnswerBusiness()
            {
                Id = 2831,
                Text = "Evidentemente non devi saperlo",
                Score = 1,
                QuestionId = 709,
                IsCorrect = AnswerState.NotCorrect
            });




            //mytest.Id = 3;
            //mytest.Date = DateTime.Now;
            //mytest.Title = "MyFirstTest"; 
            //mytest.Questions.Add(new QuestionBusiness()
            //{
            //    Text = "OkCampione",
            //    TestId = 3
            //});
            //mytest.Questions.Add(new QuestionBusiness()
            //{
            //    Text = "La vita fa schifo",
            //    TestId = 3
            //}); 
            //mytest.Questions.ElementAt(0).Answers.Add(new AnswerBusiness()
            //{
            //    Text = "WOW",
            //    IsCorrect = AnswerState.Correct,
            //    Score = 10
            //});
            //mytest.Questions.ElementAt(1).Answers.Add(new AnswerBusiness()
            //{
            //    Text = "WOW",
            //    IsCorrect = AnswerState.Correct,
            //    Score = 10
            //});

            repo.UpdateTest(mytest);

            int g = 0;

        }
    }
}
