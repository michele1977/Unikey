using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Repository;

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

            UserRepository repo = new UserRepository();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task UserRepository_FindUser_KO()
        {
            User user = new User();
            user.Username = "ugo";
            user.Password = "1234";

            UserRepository repo = new UserRepository();
            bool result = await repo.FindUser(user);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestRepository_GetQuestionById_OK()
        {
            var repo = new TestRepository();

            var question = repo.GetQuestionById(807);

            Assert.AreEqual("I vecchi cosa ammirano?", question.Result.Text);
        }

        [TestMethod]
        public async Task TestRepository_RicorsiveUpdateAndSave_OK()
        {
            var _ctx = new TestPlatformDBEntities();
            var repo = new TestRepository();
            //var mytest = new TestBusiness();

            //mytest.Id = 60;
            //mytest.Date = DateTime.Now;
            //mytest.Title = "AntaniTest";
            //mytest.Questions.Add(new QuestionBusiness()
            //{
            //    Id = 709,
            //    Text = "Dimmi come ti chiami?",
            //    TestId = 60,
                
            //});
            //mytest.Questions.Add(new QuestionBusiness()
            //{
            //    Id = 710,
            //    Text = "Da quanti anni sei nato?",
            //    TestId = 60
            //});
            //mytest.Questions.FirstOrDefault(q=>q.Id == 710).Answers.Add(new AnswerBusiness()
            //{
            //    Id = 2832,
            //    Text = "Da più dello scorso anno",
            //    Score = 100,
            //    QuestionId = 710,
            //    IsCorrect = AnswerState.Correct
            //});
            //mytest.Questions.FirstOrDefault(q => q.Id == 710).Answers.Add(new AnswerBusiness()
            //{
            //    Id = 2833,
            //    Text = "Da meno di te",
            //    Score = 1,
            //    QuestionId = 710,
            //    IsCorrect = AnswerState.NotCorrect
            //});
            //mytest.Questions.FirstOrDefault(q => q.Id == 710).Answers.Add(new AnswerBusiness()
            //{
            //    Id = 2834,
            //    Text = "Da più di mia madre",
            //    Score = 1,
            //    QuestionId = 710,
            //    IsCorrect = AnswerState.NotCorrect
            //});
            //mytest.Questions.FirstOrDefault(q => q.Id == 710).Answers.Add(new AnswerBusiness()
            //{
            //    Text = "Fatti gli affari tuoi",
            //    Score = 80,
            //    QuestionId = 710,
            //    IsCorrect = AnswerState.NotCorrect
            //});
            //mytest.Questions.FirstOrDefault(q => q.Id == 709).Answers.Add(new AnswerBusiness()
            //{
            //    Id = 2829,
            //    Text = "Mi chiamano gli altri",
            //    Score = 1,
            //    QuestionId = 709,
            //    IsCorrect = AnswerState.Correct
            //});
            //mytest.Questions.FirstOrDefault(q => q.Id == 709).Answers.Add(new AnswerBusiness()
            //{
            //    Id = 2830,
            //    Text = "Con il mio nome",
            //    Score = 1,
            //    QuestionId = 709,
            //    IsCorrect = AnswerState.NotCorrect
            //});
            //mytest.Questions.FirstOrDefault(q => q.Id == 709).Answers.Add(new AnswerBusiness()
            //{
            //    Id = 2831,
            //    Text = "Evidentemente non devi saperlo",
            //    Score = 1,
            //    QuestionId = 709,
            //    IsCorrect = AnswerState.NotCorrect
            //});

            using(var trans = _ctx.Database.BeginTransaction()) 
            {
                try
                {
                    var oldTest = await repo.GetTest(60);
                    var testToUpdate = await repo.GetTest(60);
                    testToUpdate.Title = "Prova";
                    repo.UpdateTest(testToUpdate);
                    var newTest = await repo.GetTest(60);

                    Assert.AreNotEqual(oldTest.Title, newTest.Title);
                }
                catch
                {
                    throw new Exception();
                }
                finally 
                {
                    trans.Rollback();
                }
            }
        }
    }
}
