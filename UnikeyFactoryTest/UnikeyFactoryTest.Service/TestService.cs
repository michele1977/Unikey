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
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class TestService : ITestService
    {
        private ITestRepository Repo;

        private readonly IKernel Kernel; 
       
        public TestService(ITestRepository value,IKernel kernel)
        {
            Kernel = kernel;
            Repo = value;
        }

        public void AddNewTest(TestBusiness test)
        {
            using (Repo)
            {
                if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                var mapper = Kernel.Get<IMapper>("Heavy");
                var testDaoo = mapper.Map<TestBusiness, Test>(test);
                Repo.SaveTest(testDaoo);
                    test.Id = testDaoo.Id;

            }
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

        public async Task UpdateTest(TestBusiness test)
        {
            if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                await Repo.UpdateTest(test);
            
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

        public async Task<TestBusiness> GetTestByURL(string modelUrl)
        {
            return await Repo.GetTestByURL(modelUrl);
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

        public void Dispose()
        {
            Repo.Dispose();
        }


        public async Task UpdateQuestion(QuestionBusiness updateQuestion)
        {
            await Repo.UpdateQuestion(updateQuestion);
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

        public async Task<Dictionary<int, int>> GetExTestCountByState(IEnumerable<int> testsIds, AdministratedTestState state)
        {
            return await Repo.GetExTestCountByState(testsIds, state);
        }
    }
}
