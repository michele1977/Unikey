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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly TestPlatformDBEntities _ctx;

        public QuestionRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public void SaveQuestions(List<Question> questions)
        {
            foreach (var question in questions)
            {
                _ctx.Questions.Add(question);
            }
            _ctx.SaveChanges();
        }
    }
}
