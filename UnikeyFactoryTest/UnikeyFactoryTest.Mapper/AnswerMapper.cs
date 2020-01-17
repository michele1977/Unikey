
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;

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
                IsCorrect = (AnswerState)answer.IsCorrect,
                Score = answer.Score
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
                IsCorrect = (byte)answer.IsCorrect,
                Score = answer.Score
            };

            return returned;
        }
    }
}
