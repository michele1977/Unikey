using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public static class AdministratedQuestionMapper
    {
        public static AdministratedQuestionBusiness MapDaoToDomain(AdministratedQuestion dao)
        {
            var returned = new AdministratedQuestionBusiness
            {
                Id = dao.Id,
                Text = dao.Text,
                AdministratedTestId = dao.AdministratedTestId,
                AdministratedTest = AdministratedTestMapper.MapDaoToDomain(dao.AdministratedTest),
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
                AdministratedTest = AdministratedTestMapper.MapDomainToDao(domain.AdministratedTest),
                AdministratedAnswers = domain.AdministratedAnswers.Select(AdministratedAnswerMapper.MapDomainToDao).ToList()
            };

            return returned;
        }
    }
}
