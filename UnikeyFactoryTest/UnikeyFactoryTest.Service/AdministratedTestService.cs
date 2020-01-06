using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AdministratedTestService
    {
        private readonly IAdministratedTestRepository _repo;

        public AdministratedTestService()
        {
            _repo = new AdministratedTestRepository();
        }

        public AdministratedTestService(AdministratedTestRepository repo)
        {
            if(repo is null)
                _repo = new AdministratedTestRepository();
            else
                _repo = repo;
        }

        public AdministratedTestBusiness AdministratedTest_Builder(TestBusiness test, string subject)
        {
            var newAdTest = new AdministratedTestBusiness();

            newAdTest.Date = DateTime.Today;
            newAdTest.URL = test.URL;
            newAdTest.TestId = test.Id;
            newAdTest.TestSubject = subject;
            newAdTest.AdministratedQuestions = new List<AdministratedQuestionBusiness>();
            foreach (var q in test.Questions)
            {
                newAdTest.AdministratedQuestions.Add(new AdministratedQuestionBusiness()
                {
                    Text = q.Text,
                    AdministratedTestId = q.TestId,
                    AdministratedAnswers = q.Answers.Select(a => new AdministratedAnswerBusiness() { Text = a.Text, Score = a.Score, AdministratedQuestionId = a.QuestionId, isCorrect = a.IsCorrect, isSelected = false }).ToList()
                });
            }

            return newAdTest;
        }

        public void Add(AdministratedTestBusiness adTest)
        {
            _repo.Add(adTest);
        }

        public void Update_Save(AdministratedTestBusiness adTest)
        {
            decimal score = 0;

            foreach (var q in adTest.AdministratedQuestions)
            {
                if ((q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)) != null)
                    score = score + q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true).Score??0;
            }

            adTest.TotalScore = decimal.ToInt32(score);

            _repo.Update_Save(adTest);
        }

        public AdministratedTestBusiness GetAdministratedTestById (int adTestId)
        {
           return _repo.GetAdministratedTestById(adTestId);
        }
    }
}
