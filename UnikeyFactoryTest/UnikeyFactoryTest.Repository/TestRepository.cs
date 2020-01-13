using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
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
            var result = _ctx.Tests.FirstOrDefault(x => x.URL.Equals(URL));
            if (result == null) throw new Exception("Not valid URL");
            else return TestMapper.MapDalToBizLight(result);
        }

        public TestBusiness GetTest(int testId)
        {
            Test test = _ctx.Tests.FirstOrDefault(t => t.Id == testId);

            if (test == null)
            {
                throw new NullReferenceException("Test not found at specified id");
            }

            return TestMapper.MapDalToBizHeavy(test);
        }

        public List<TestBusiness> GetTests()
        {
            var returned = _ctx.Tests.ToList();
            var res = from test in _ctx.Tests
                      join quest in _ctx.Questions
                          on test.Id equals quest.TestId
                      group test by new
                      {
                          Id = test.Id, 
                          Url = test.URL, 
                          Date = test.Date, 
                          NumQuestions = _ctx.Questions.Count(q => q.TestId == test.Id)
                      } into temp
                      select new { Id = temp.Key.Id, URL = temp.Key.Url, Date = temp.Key.Date, NumQuestions = temp.Key.NumQuestions };

            List<Test> returned1 = new List<Test>();

            foreach (var test in res)
            {
                returned1.Add(new Test()
                {
                    Id = test.Id,
                    URL = test.URL,
                    Date = test.Date,
                    NumQuestions = test.NumQuestions,
                });
            }
            
            return returned1.Select(TestMapper.MapDalToBizLight).ToList();
        }

        public void DeleteTest(int testId)
        {
            Test test = _ctx.Tests.FirstOrDefault(t => t.Id == testId);
            if (test == null)
            {
                throw new NullReferenceException("Test not found at specified id");
            }
            _ctx.Tests.Remove(test);
            _ctx.SaveChanges();
        }

        public void UpdateTest(TestBusiness test)
        {
            if (test == null)
            {
                throw new NullReferenceException("No test to update");
            }

            Test mappedTest = TestMapper.MapBizToDal(test);

            Test testToUpdate = _ctx.Tests.FirstOrDefault(t => t.Id == test.Id);
            testToUpdate.URL = mappedTest.URL;
            testToUpdate.User = mappedTest.User;
            testToUpdate.Date = mappedTest.Date;
            testToUpdate.UserId = mappedTest.UserId;
            testToUpdate.AdministratedTests = mappedTest.AdministratedTests;
            testToUpdate.Questions = mappedTest.Questions;

            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
