using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public class AdministratedQuestionMapper
    {
        public static AdministratedQuestionBusiness MapDaoToDomain(AdministratedQuestion dao)
        {
            var returned = new AdministratedQuestionBusiness
            {
                Id = dao.Id,
                Text = dao.Text,
                AdministratedTestId = dao.AdministratedTestId,
                AdministratedAnswers = dao.AdministratedAnswers.Select(AdministratedAnswerMapper.MapDaoToDomain).ToList()
            };

            return returned;
        }

        public static AdministratedQuestion MapDomainToDao(AdministratedQuestionBusiness domain)
        {
            var returned = new AdministratedQuestion
            {
                Id = domain.Id,
                Text = domain.Text,
                AdministratedTestId = domain.AdministratedTestId,
                AdministratedAnswers = domain.AdministratedAnswers.Select(AdministratedAnswerMapper.MapDomainToDao).ToList()
            };

            return returned;
        }
    }
}
