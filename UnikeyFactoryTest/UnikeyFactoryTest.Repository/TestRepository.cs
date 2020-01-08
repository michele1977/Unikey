using System;
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

        public void SaveTest(Test test)
        {
            _ctx.Tests.Add(test);
            _ctx.SaveChanges();
        }


        public string GenerateUrl()
        {
            var myGuid = Guid.NewGuid();
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            return $"{baseUrl}ExTest\\TestStart?guid={myGuid.ToString()}";
        }

        public TestBusiness GetTestByURL(string URL)
        {
            var result = _ctx.Tests.FirstOrDefault(x => x.URL.Equals(URL));
            if (result == null) throw new Exception("Not valid URL");
            else return TestMapper.MapDalToBiz(result);
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

        public List<TestBusiness> GetTests()
        {
            var returned =  _ctx.Tests.ToList();
            //var returned1 =  returned.Select(x => TestMapper.MapDalToBizLigth(x)).ToList();
            return returned.Select(TestMapper.MapDalToBizLigth).ToList();
        }

        private  TestBusiness MapDalToBizLigth2(TestBusiness test)
        {
            var returned = new TestBusiness
            {
                Id = test.Id,
                URL = test.URL,
                Date = test.Date,
                UserId = test.UserId,
            };
            return returned;
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
