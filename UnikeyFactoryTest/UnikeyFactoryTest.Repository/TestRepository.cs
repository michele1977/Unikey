using System;
using System.Collections.Generic;
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

        public void SaveTest(Test test)
        {
            _ctx.Tests.Add(test);
            _ctx.SaveChanges();
        }

        public TestBusiness GetTestByURL(string URL)
        {
            throw new NotImplementedException();
            //var result = _ctx.Tests.FirstOrDefault(x => x.URL.Equals(URL));
            //if (result == null) throw new Exception("Not valid URL");
            //else return /*mapping*/ result;
        }

        public Test GetTest(int testId)
        {
            Test test = _ctx.Tests.FirstOrDefault(t => t.Id == testId);

            if (test == null)
            {
                throw new NullReferenceException("Test not found at specified id");
            }
            return test;
        }

        public IEnumerable<Test> GetTests()
        {
            return _ctx.Tests;
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
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
