using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class TestService
    {
        public ITestRepository Repo { get; set; }

        public TestService()
        {
            Repo = new TestRepository();
        }

        public async Task AddNewTest(TestBusiness test)
        {
            using (var _repo = new TestRepository())
            {
                if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                var testDao = TestMapper.MapBizToDal(test);
                await _repo.SaveTest(testDao);
                test.Id = testDao.Id;

            }
        }

        public async Task<TestBusiness> GetTestById(int testId)
        {
            Repo = new TestRepository();

            TestBusiness test = null;

            test = await Repo.GetTest(testId);

            return test;
        }

        public async Task<List<TestBusiness>> GetTests()
        {
            Repo = new TestRepository();
            var tests = Repo.GetTests();
            return await tests;
        }

        public async Task DeleteTest(int testId)
        {
            using (Repo = new TestRepository())
            {
                await Repo.DeleteTest(testId);
            }
        }

        public void UpdateTest(TestBusiness test)
        {
            using (TestRepository _repo = new TestRepository())
            {
                if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                _repo.UpdateTest(test);
            }
        }


        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateUrl(string guid)
        {
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            return $"{baseUrl}ExTest\\TestStart?guid={guid.ToString()}";

        }


        public TestBusiness GetTestByURL(string modelUrl)
        {
            using (TestRepository _repo = new TestRepository())
            {
                return _repo.GetTestByURL(modelUrl);
            }
        }

        public async Task DeleteQuestionByIdFromTest(int questionId)
        {
            using (Repo = new TestRepository())
            {
                await Repo.DeleteQuestionByIdFromTest(questionId);
            }
        }

        public async Task<List<TestBusiness>> GetTestsByFilter(string filter)
        {
            using (Repo = new TestRepository())
            {
                var res = (await Repo.GetTests()).Where(t => t.Title.ToLower().Contains(filter.ToLower())).ToList();
                return res;
            }
        }

        public async Task<QuestionBusiness> GetQuestionById(int id)
        {
            using (Repo = new TestRepository())
            {
                return await Repo.GetQuestionById(id);
            }
        }
        public void UpdateQuestion(QuestionBusiness question)
        {
            using (Repo = new TestRepository())
            {
                Repo.UpdateQuestion(question);
            }
        }
        public async void AddOrUpdateQuestion(QuestionBusiness question)
        {
            using (Repo = new TestRepository())
            {
                //se id = allora devo aggiungere la domanda
                if (question.Id == 0)
                {
                    var test = await GetTestById(question.TestId);
                    if (test.Questions.Count == 0)
                    {
                        question.Position = 0;
                    }
                    else
                    {
                        question.Position = Convert.ToInt16(test.Questions.Count);
                    }
                    test.Questions.Add(question);

                    UpdateTest(test);
                }
                //Se id != 0 allora devo aggiornare la domanda
                if (question.Id != 0)
                {
                    UpdateQuestion(question);
                }
            }
        }

    }
}
