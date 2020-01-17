using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AdministratedTestService
    {
        private IAdministratedTestRepository _repo;

        public int QuestionPosition = 0;

        public bool IsLast = false;

        public AdministratedTestService()
        {
            _repo = new AdministratedTestRepository();
        }

        public AdministratedTestService(IAdministratedTestRepository repo)
        {
            if (repo is null)
                _repo = new AdministratedTestRepository();
            else
                _repo = repo;
        }

        public AdministratedTestBusiness AdministratedTest_Builder(TestBusiness test, string subject)
        {
            var newAdTest = new AdministratedTestBusiness
            {
                Date = DateTime.Today,
                URL = test.URL,
                TestId = test.Id,
                TestSubject = subject,
                AdministratedQuestions = new List<AdministratedQuestionBusiness>(),
                State = (AdministratedTestState)1
            };

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

        public async Task<AdministratedTestBusiness> Add(AdministratedTestBusiness adTest)
        {
            return await _repo.Add(adTest);
        }

        public async Task Update_Save(AdministratedTestBusiness adTest)
        {
            await _repo.Update_Save(adTest);
        }

        public async Task<AdministratedTestBusiness> GetAdministratedTestById(int adTestId)
        {
            return await _repo.GetAdministratedTestById(adTestId);
        }

        //public async Task<AdministratedTestBusiness> GetAdministratedTest(int administratedTestId)
        //{
        //    _repo = new AdministratedTestRepository();

        //    try
        //    {
        //        return await _repo.GetAdministratedTestById(administratedTestId);
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        //TODO
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        //TODO
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO
        //    }

        //}

        public async Task<IEnumerable<AdministratedTestBusiness>> GetAdministratedTests()
        {
            _repo = new AdministratedTestRepository();
            var administratedTests = await _repo.GetAdministratedTests();

            return administratedTests;
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int testId)
        {
            _repo = new AdministratedTestRepository();
            var myTask = Task.Run(() => _repo.GetAdministratedTestsByTestId(testId));
            return await myTask;
        }

        public async Task DeleteAdministratedTest(int administratedTestId)
        {
            using (_repo = new AdministratedTestRepository())
            {
                await _repo.DeleteAdministratedTest(administratedTestId);
            }
        }

        public async Task<AdministratedQuestionBusiness> Next(int AdministratedTestId, int position)
        {
            var test = await _repo.GetAdministratedTestById(AdministratedTestId);
            return test.AdministratedQuestions.FirstOrDefault(x => x.Position == position);
        }

        public async Task Update_Save_Question(AdministratedQuestionBusiness adQuestion)
        {
            await _repo.Update_Save_Question(adQuestion);
        }
        public async Task<AdministratedQuestionBusiness> Previous(int AdministratedTestId, int position)
        {
            var test = await _repo.GetAdministratedTestById(AdministratedTestId);
            return test.AdministratedQuestions.FirstOrDefault(x => x.Position == position);
        }
    }
}
