using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public class AdministratedAnswerMapper
    {
        public static AdministratedAnswer MapDaoToDomain(Context.AdministratedAnswer dao)
        {
            var returned = new AdministratedAnswer
            {
                Id = dao.Id,
                Text = dao.Text,
                isCorrect = dao.isCorrect,
                isSelected = dao.isSelected,
                Score = dao.Score
            };

            return returned;
        }

        public static Context.AdministratedAnswer MapDomainToDao(AdministratedAnswer domain)
        {
            var returned = new Context.AdministratedAnswer
            {
                Id = domain.Id,
                Text = domain.Text,
                isCorrect = domain.isCorrect,
                isSelected = domain.isSelected,
                AdministratedQuestionId = domain.AdministratedQuestionId,
                Score = domain.Score
            };

            return returned;
        }
    }
}
