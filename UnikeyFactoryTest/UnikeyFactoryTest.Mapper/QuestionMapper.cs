using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public class QuestionMapper
    {
        public static QuestionBusiness MapDalToBiz(Question question)
        {
            var returned = new QuestionBusiness()
            {
                Id = question.Id,
                Text = question.Text,
                TestId = question.TestId,
                Answers = question.Answers.Select(AnswerMapper.MapDalToBiz).ToList(),
            };

            return returned;
        }

        public static Question MapBizToDal(QuestionBusiness question)
        {
            var returned = new Question()
            {
                Id = question.Id,
                Text = question.Text,
                TestId = question.TestId,
                Answers = question.Answers.Select(AnswerMapper.MapBizToDal).ToList(),
            };

            return returned;
        }
    }
}
