using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class TestService : ITestService
    {
        private ITestRepository Repo;
        //public TestService()
        //{
        //    Repo = new TestRepository();
        //}
        public TestService(ITestRepository value)
        {
            Repo = value;
        }

        public async Task AddNewTest(TestBusiness test)
        {
            if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                var testDao = TestMapper.MapBizToDal(test);
                    await Repo.SaveTest(testDao);
                    test.Id = testDao.Id;
        }
        public async Task <TestBusiness> GetTestById(int testId)
        {
            TestBusiness test = null;

            test = await Repo.GetTest(testId);

            return test;
        }
        public async Task<List<TestBusiness>> GetTests()
        { 
            var tests =  Repo.GetTests();
            return await tests;
        }
        public async Task DeleteTest(int testId)
        {
                await Repo.DeleteTest(testId);
        }
        public void UpdateTest(TestBusiness test)
        {
            if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                Repo.UpdateTest(test);
            
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
            return Repo.GetTestByURL(modelUrl);
        }
        public async Task DeleteQuestionByIdFromTest(int questionId)
        {
            await Repo.DeleteQuestionByIdFromTest(questionId);
        }
        public async Task<List<TestBusiness>> GetTestsByFilter(string filter)
        {
            var res = (await Repo.GetTests()).Where(t => t.Title.ToLower().Contains(filter.ToLower())).ToList();
            return res;
        }

        public async Task<QuestionBusiness> GetQuestionById(int id)
        {
            return await Repo.GetQuestionById(id);
        }

        public async Task<Dictionary<int, int>> OpenedTestNumber(List<int> TestsId)
        {
            return await Repo.OpenedTestNumber(TestsId);
        }
        public void Dispose()
        {
            Repo.Dispose();
        }


        public Dictionary<int, int> GetClosedTests(int pageNum, int pageSize, string filter)
        {
            var result = Repo.GetClosedTests(pageNum,pageSize, filter);

            return result;
        }

    }
}
