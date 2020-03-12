using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AdministratedTestService : IAdministratedTestService
    {
        private IAdministratedTestRepository _repo;
        private readonly IKernel _kernel;

        public int QuestionPosition = 0;
        public bool IsLast = false;

        public AdministratedTestService(IAdministratedTestRepository repo, IKernel kernel)
        {
            _repo = repo;
            _kernel = kernel;
        }

        public AdministratedTestBusiness AdministratedTest_Builder(TestBusiness test, string subject)
        {
            var newAdTest = new AdministratedTestBusiness
            {
                Date = DateTime.Now,
                URL = test.URL,
                TestId = test.Id,
                TestSubject = subject,
                Title = test.Title,
                AdministratedQuestions = new List<AdministratedQuestionBusiness>(),
                State = AdministratedTestState.Open
            };
            foreach (var q in test.Questions)
            {
                newAdTest.AdministratedQuestions.Add(new AdministratedQuestionBusiness()
                {
                    Text = q.Text,
                    AdministratedTestId = q.TestId,
                    Position = q.Position,
                    AdministratedAnswers = q.Answers.Select(a => new AdministratedAnswerBusiness() { Text = a.Text, Score = a.Score, AdministratedQuestionId = a.QuestionId, isCorrect = a.IsCorrect, isSelected = false }).ToList()
                });

                newAdTest.MaxScore += q.Answers.SingleOrDefault(a => a.IsCorrect == AnswerState.Correct).Score;
            }

            return newAdTest;
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTestsByFilter(string textFilter)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            {
                var res = (await _repo.GetAdministratedTests()).Where(t => t.TestSubject.ToLower().Contains(textFilter.ToLower())).ToList();
                return res;
            }
        }

        public AdministratedTestBusiness Add(AdministratedTestBusiness adTest)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            {
                return _repo.Add(adTest);
            }
        }

        public async Task Update_Save(AdministratedTestBusiness adTest)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            { 
                await _repo.Update_Save(adTest);
            }
        }

        public async Task<AdministratedTestBusiness> GetAdministratedTestById(int adTestId)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            {
                return await _repo.GetAdministratedTestById(adTestId);
            }
        }

        public async Task<IEnumerable<AdministratedTestBusiness>> GetAdministratedTests()
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            {
                var administratedTests = await _repo.GetAdministratedTests();
                return administratedTests;
            }
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int pageNum, int pageSize, string filter, int testId)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            {
                var lastPage = (int)Math.Ceiling((float)await CountExTests(filter) / pageSize);

                if (String.IsNullOrWhiteSpace(filter))
                    return await _repo.GetAdministratedTestsByTestId(pageNum, pageSize, testId);
                else
                    return await _repo.GetAdministratedTestsByTestId(pageNum, pageSize, filter, testId);
            }
        }

        public async Task DeleteAdministratedTest(int administratedTestId)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            {
                await _repo.DeleteAdministratedTest(administratedTestId);
            }
        }

        public AdministratedQuestionBusiness Next(AdministratedTestBusiness administratedTest, int position)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            return administratedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == position);
        }

        public async Task Update_Save_Question(AdministratedQuestionBusiness adQuestion)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            using (_repo)
            {
                await _repo.Update_Save_Question(adQuestion);
            }
        }

        public AdministratedQuestionBusiness Previous(AdministratedTestBusiness administratedTest, int position)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }
            return administratedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == position);
        }

        public async Task<int> CountExTests(string filter)
        {
            if (_repo.IsContextNull)
            {
                _repo = _kernel.Get<IAdministratedTestRepository>();
            }

            if (String.IsNullOrWhiteSpace(filter))
                return await _repo.CountExTests();
            else
                return await _repo.CountExTests(filter);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}