using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;

namespace UnikeyFactoryTest.Repository
{
    public class QuestionRepository : IQuestionRepository, IDisposable
    {
        private readonly TestPlatformDBEntities _ctx;

        public QuestionRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public void SaveQuestion(Question question)
        {
            _ctx.Questions.Add(question);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
