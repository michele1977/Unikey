using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AdministratedTestService
    {
        private readonly IAdministratedTestRepository repo;

        public AdministratedTestService()
        {
            repo = new AdministratedTestRepository();
        }

        public AdministratedTestBusiness AdministratedTest_Builder(TestBusiness test, string subject )
        {
            var newAdTest = new AdministratedTest();

            newAdTest.Date = DateTime.Today;
            newAdTest.URL = test.URL;
            newAdTest.TestId = test.Id;
            newAdTest.TestSubject = subject;
            foreach (var q in test.Questions)
            {
                newAdTest.AdministratedQuestions.Add(new AdministratedQuestionBusiness()
                {
                    Text = q.Text,
                    AdministratedTestId = q.TestId,
                    AdministratedAnswers = q.Answers.Select(a=> new AdministratedAnswerBusiness(){Text = a.Text, Score = a.Score, AdministratedQuestionId = a.QuestionId}).ToList()
                });
            }

            return newAdTest;
        }

        public void Save(AdministratedTestBusiness adTest)
        {
            repo.Add(adTest);
        }

        public void Update_Save(AdministratedTestBusiness adTest)
        {
            repo.Update_Save(adTest);
        }

        public AdministratedTestBusiness GetAdministratedTestById (int adTestId)
        {
           return repo.GetAdministratedTestById(adTestId);
        }
    }
}
