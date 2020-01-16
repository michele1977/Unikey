
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public class AnswerMapper
    {
        public static AnswerBusiness MapDalToBiz(Answer answer)
        {
            var returned = new AnswerBusiness()
            {
                Id = answer.Id,
                Text = answer.Text,
                QuestionId = answer.QuestionId,
                IsCorrect = answer.IsCorrect.HasValue ? answer.IsCorrect.Value : false,
                Score = answer.Score.HasValue ? answer.Score.Value : 0
            };

            return returned;
        }

        public static Answer MapBizToDal(AnswerBusiness answer)
        {
            var returned = new Answer()
            {
                Id = answer.Id,
                Text = answer.Text,
                QuestionId = answer.QuestionId,
                IsCorrect = answer.IsCorrect,
                Score = answer.Score
            };

            return returned;
        }
    }
}
