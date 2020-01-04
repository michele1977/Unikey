using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.Mapper
{
    public class AdministratedAnswerMapper
    {
        public static AdministratedAnswerBusiness MapDaoToDomain(AdministratedAnswer dao)
        {
            var returned = new AdministratedAnswerBusiness
            {
                Id = dao.Id,
                Text = dao.Text,
                isCorrect = dao.isCorrect,
                isSelected = dao.isSelected,
                Score = dao.Score,
                AdministratedQuestionId = dao.AdministratedQuestionId,
            };

            return returned;
        }

        public static AdministratedAnswer MapDomainToDao(AdministratedAnswerBusiness domain)
        {
            var returned = new AdministratedAnswer
            {
                Id = domain.Id,
                Text = domain.Text,
                isCorrect = domain.isCorrect,
                isSelected = domain.isSelected,
                Score = domain.Score,
                AdministratedQuestionId = domain.AdministratedQuestionId,
            };

            return returned;
        }
    }
}
