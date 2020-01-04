using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
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

        public AdministratedTestBusiness AdministratedTest_Builder(Test test, string subject )
        {
            var newAdTest = new AdministratedTestBusiness();

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
                    AdministratedAnswers = q.Answers.Select(a=> new AdministratedAnswerBusiness(){Text = a.Text, Score = a.Score, AdministratedQuestionId = a.QuestionId, isCorrect = a.IsCorrect, isSelected = false}).ToList()
                });
            }

            return newAdTest;
        }

        public void Add(AdministratedTestBusiness adTest)
        {
            repo.Add(AdministratedTestMapper.MapDomainToDao(adTest));
        }

        public void Update_Save(AdministratedTestBusiness adTest)
        {
            decimal score = 0;

            foreach (var q in adTest.AdministratedQuestions)
            {
                if ((q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)) != null)
                    score = score + q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true).Score??0;
            }

            adTest.TotalScore = Decimal.ToInt32(score);

            //repo.Update_Save(adTest);
        }

        public AdministratedTestBusiness GetAdministratedTestById (int adTestId)
        {
           return AdministratedTestMapper.MapDaoToDomain(repo.GetAdministratedTestById(adTestId));
        }
    }
}
