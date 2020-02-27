using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using Microsoft.AspNet.Identity;
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
        private ITestRepository _repo;

        private readonly IKernel _kernel;

        private readonly int userId;

        public TestService(ITestRepository value, IKernel kernel)
        {
            _kernel = kernel;
            _repo = value;
        }

        public async Task AddNewTest(TestBusiness test)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            if (string.IsNullOrWhiteSpace(test.URL)) 
                throw new Exception("Test not saved");

                var mapper = _kernel.Get<IMapper>("Heavy");
                var testDao = mapper.Map<TestBusiness, Test>(test);
                IUserRepository ur = _kernel.Get<IUserRepository>();
                var user = await ur.FindByIdAsync(30);
                testDao.User =  mapper.Map<UserBusiness, User>(user);
                _repo.SaveTest(testDao);
                test.Id = testDao.Id;
        }


        public async Task<TestBusiness> GetTestById(int testId)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            using (_repo)
            {
                var test = await _repo.GetTest(testId);
                return test;
            }
        }

        public async Task<List<TestBusiness>> GetTests()
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            var tests = await _repo.GetTests();
            return tests;
        }

        public async Task DeleteTest(int testId)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            await _repo.DeleteTest(testId);
        }

        public async Task UpdateTest(TestBusiness test)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            if (string.IsNullOrWhiteSpace(test.URL)) 
                throw new Exception("Invalid test to update");
            
            await _repo.UpdateTest(test);

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
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            return await _repo.GetTestByURL(modelUrl);
        }

        public async Task DeleteQuestionByIdFromTest(int questionId)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            await _repo.DeleteQuestionByIdFromTest(questionId);
        }

        public async Task<List<TestBusiness>> GetTestsByFilter(string filter)
        {
            var res = (await _repo.GetTests()).Where(t => t.Title.ToLower().Contains(filter.ToLower())).ToList();
            return res;
        }

        public async Task<QuestionBusiness> GetQuestionById(int id)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            return await _repo.GetQuestionById(id);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }

        public async Task UpdateQuestion(QuestionBusiness updateQuestion)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            await _repo.UpdateQuestion(updateQuestion);
        }

        public async Task AddOrUpdateQuestion(QuestionBusiness question)
        {
            if (question.Id == 0)
            {
                var test = await GetTestById(question.TestId);
                question.Position = Convert.ToInt16(test.Questions.Count);
                test.Questions.Add(question);

                await UpdateTest(test);
            }

            if (question.Id != 0)
            {
                await UpdateQuestion(question);
            }
        }

        public async Task<Dictionary<int, int>> GetExTestCountByState(IEnumerable<int> testsIds, AdministratedTestState state)
        {
            if (_repo.IsContextNull) _repo = _kernel.Get<ITestRepository>();

            return await _repo.GetExTestCountByState(testsIds, state);
        }

        public StringBuilder TextBuilder(QuestionBusiness question, StringBuilder sb, int position)
        {
            sb.Append($"{position}.{question.Text}\n\n");

            foreach (var answer in question.Answers)
            {
                sb.Append($"□ {answer.Text}\n");
            }

            sb.Append("\n");

            return sb;
        }

        public void ClipBoardMethod(StringBuilder sb)
        {
            Thread thread = new Thread(() => Clipboard.SetText(sb.ToString()));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
