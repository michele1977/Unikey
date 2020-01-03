using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public class AdministratedQuestionMapper
    {
        public static AdministratedQuestion MapDaoToDomain(Context.AdministratedQuestion dao)
        {
            var returned = new AdministratedQuestion
            {
                Id = dao.Id,
                Text = dao.Text,
                AdministratedTestId = dao.AdministratedTestId,
                AdministratedTest = AdministratedTestMapper.MapDaoToDomain(dao.AdministratedTest),
                AdministratedAnswers = new List<AdministratedAnswer>()
            };

            foreach(var aa in dao.AdministratedAnswers)
            {
                returned.AdministratedAnswers.Add(new AdministratedAnswer
                {
                    Id = aa.Id,
                    Text = aa.Text,
                    isCorrect = aa.isCorrect,
                    isSelected = aa.isSelected,
                    Score = aa.Score,
                    AdministratedQuestionId = aa.AdministratedQuestionId
                });
            }

            return returned;
        }

        public static Context.AdministratedQuestion MapDomainToDao(AdministratedQuestion domain)
        {
            var returned = new Context.AdministratedQuestion
            {
                Id = domain.Id,
                Text = domain.Text,
                AdministratedTestId = domain.AdministratedTestId,
                AdministratedTest = AdministratedTestMapper.MapDomainToDao(domain.AdministratedTest),
                AdministratedAnswers = new List<Context.AdministratedAnswer>()
            };

            foreach(var aa in domain.AdministratedAnswers)
            {
                returned.AdministratedAnswers.Add(new Context.AdministratedAnswer
                {
                    Id = aa.Id,
                    Text = aa.Text,
                    isCorrect = aa.isCorrect,
                    isSelected = aa.isSelected,
                    Score = aa.Score,
                    AdministratedQuestionId = aa.AdministratedQuestionId
                });
            }

            return returned;
        }
    }
}
