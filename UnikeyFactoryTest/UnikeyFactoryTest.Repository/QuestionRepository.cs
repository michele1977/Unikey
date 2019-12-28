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

        public void AddAnswers(Question question, List<Answer> answers)
        {
            question.Answers = answers;
        }

        public void SaveQuestions(List<Question> questions)
        {
            _ctx.Questions.AddRange(questions);
        }
    }
}
