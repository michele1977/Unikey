using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IRepository;

namespace UnikeyFactoryTest.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly TestPlatformDBEntities _ctx;

        public AnswerRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }
        
        public void SaveAnswers(Answer answer)
        {
            _ctx.Answers.Add(answer); 
            _ctx.SaveChanges();
        }
    }
}
