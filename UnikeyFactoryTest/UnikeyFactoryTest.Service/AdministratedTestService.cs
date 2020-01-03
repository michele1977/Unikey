using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AdministratedTestService
    {
        private readonly AdministratedTestRepository repo;

        public AdministratedTestService()
        {
            repo = new AdministratedTestRepository();
        }

        public Domain.AdministratedTest AdministratedTest_Builder(Domain.Test test, string subject )
        {
            var newAdTest = new AdministratedTest();

            newAdTest.Date = DateTime.Today;
            newAdTest.URL = test.URL;
            newAdTest.TestId = test.Id;
            newAdTest.TestSubject = subject;
            foreach (var q in test.Questions)
            {
                newAdTest.AdministratedQuestions.Add(new AdministratedQuestion()
                {
                    Text = q.Text,
                    AdministratedTestId = q.TestId,
                    AdministratedAnswers = q.Answers.Select(a=> new AdministratedAnswer(){Text = a.Text, Score = a.Score, AdministratedQuestionId = a.QuestionId}).ToList()
                });
            }

            return newAdTest;
        }

        public void Save(Domain.AdministratedTest adTest)
        {
            repo.Add(adTest);
        }

        public void Update_Save(Domain.AdministratedTest adTest)
        {
            repo.Update_Save(adTest);
        }

        public Domain.AdministratedTest GetAdministratedTestById (int adTestId)
        {
           return repo.GetAdministratedTestById(adTestId);
        }
    }
}
