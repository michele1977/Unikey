using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;

namespace UnikeyFactoryTest.Repository
{
    public class TestRepository : ITestRepository, IDisposable
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

        public void SaveQuestion(Question question)
        {
            _ctx.Questions.Add(question);
            _ctx.SaveChanges();
        }

        public void SaveAnswers(List<Answer> answers)
        {
            foreach (var answer in answers)
            {
                _ctx.Answers.Add(answer);
            }

            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
