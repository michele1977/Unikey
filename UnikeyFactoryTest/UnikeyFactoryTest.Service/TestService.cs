using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using UnikeyFactoryTest.Context;
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
        private static readonly BindingsService _bindings = new BindingsService();

        private readonly IKernel Kernel = new StandardKernel(_bindings);
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
            using (Repo)
            {
                if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                var x = Kernel.Get<IMapper>("Heavy");
                var testDaoo = x.Map<TestBusiness, Test>(test);
                //var testDao = TestMapper.MapBizToDal(test);
                    await Repo.SaveTest(testDaoo);
                    test.Id = testDaoo.Id;

            }
        }
        public async Task <TestBusiness> GetTestById(int testId)
        {
            Repo = new TestRepository();

            TestBusiness test = null;

            test = await Repo.GetTest(testId);

            return test;
        }
        public async Task<List<TestBusiness>> GetTests()
        {
            Repo = new TestRepository();
            var tests =  Repo.GetTests();
            return await tests;
        }
        public async Task DeleteTest(int testId)
        {
            using (Repo)
            {
                await Repo.DeleteTest(testId);
            }
        }
        public void UpdateTest(TestBusiness test)
        {
            using (Repo)
            {
                if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                Repo.UpdateTest(test);
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
            using (Repo)
            {
                return Repo.GetTestByURL(modelUrl);
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

    }
}
