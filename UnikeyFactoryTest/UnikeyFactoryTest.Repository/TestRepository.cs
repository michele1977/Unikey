using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTest.Repository
{



    public class TestRepository : ITestRepository
    {
        private readonly TestPlatformDBEntities _ctx;

        public TestRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public async Task SaveTest(Test test)
        {
            var myTask = Task.Run(() => {
                _ctx.Tests.Add(test);
                _ctx.SaveChanges();
            });
            await myTask;
        }


        public TestBusiness GetTestByURL(string URL)
        {
            var result = _ctx.Tests.First(x => x.URL.Equals(URL));

            if (result == null)
            {
                throw new Exception($"Test not found at specified URL ({URL})");
            }

            return TestMapper.MapDalToBizHeavy(result);
        }

        public async Task<TestBusiness> GetTest(int testId)
        {
            var myTask = await Task.Run(() => _ctx.Tests.First(t => t.Id == testId));

            if (myTask == null)
            {
                throw new Exception($"Test not found at specified id ({testId})");
            }

            return TestMapper.MapDalToBizHeavy(myTask);
        }

        public async Task<List<TestBusiness>> GetTests()
        {
            var returned =  _ctx.Tests.ToList();

            var  testListTask = await Task.Run(() => _ctx.Tests.Select(t => new TestBusiness()
            {
                Id = t.Id,
                URL = t.URL,
                Date = t.Date,
                NumQuestions = t.Questions.Count
            }).ToList());

            return testListTask;
        }

        public async Task DeleteTest(int testId)
        {
            var task = await Task.Run(() =>
            {
                return _ctx.Tests.First(t => t.Id == testId);
            });

            _ctx.Tests.Remove(task);
            _ctx.SaveChanges();
        }



        public async void UpdateTest(TestBusiness test)
        {
            if (test == null)
            {
                throw new NullReferenceException("No test to update");
            }

            var mappedTest = await Task.Run(() => TestMapper.MapBizToDal(test));

            var testToUpdate = await Task.Run(() => _ctx.Tests.FirstOrDefault(t => t.Id == test.Id));

            testToUpdate.URL = mappedTest.URL;
            testToUpdate.User = mappedTest.User;
            testToUpdate.Date = mappedTest.Date;
            testToUpdate.UserId = mappedTest.UserId;
            testToUpdate.AdministratedTests = mappedTest.AdministratedTests;
            testToUpdate.Questions = mappedTest.Questions;

            _ctx.SaveChanges();
        }
        public async Task DeleteQuestion(int questionId)
        {
            var question = await Task.Run(() =>
            {
                return _ctx.Questions.FirstOrDefault(q => q.Id == questionId);
            });
            if (question == null)
            {
                throw new NullReferenceException("Question not found ");
            }
            _ctx.Questions.Remove(question);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
