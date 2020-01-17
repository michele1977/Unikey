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
    public class AdministratedTestMapper 
    {
        public static AdministratedTestBusiness MapDaoToDomainLight(AdministratedTest dao)
        {
            var returned = new AdministratedTestBusiness
            {
                Id = dao.Id,
                URL = dao.URL,
                Date = dao.Date,
                Title = dao.Title,
                TestId = dao.TestId.Value,
                TestSubject = dao.TestSubject,
                State = (AdministratedTestState) dao.State
                
            };
            return returned;
        }

        public static AdministratedTestBusiness MapDaoToDomain(AdministratedTest dao)
        {
            var returned = new AdministratedTestBusiness
            {
                Id = dao.Id,
                URL = dao.URL,
                TestId = dao.TestId.Value,
                Title = dao.Title,
                TestSubject = dao.TestSubject,
                Date = dao.Date,
                State = (AdministratedTestState) dao.State, 
                AdministratedQuestions = dao.AdministratedQuestions.Select(AdministratedQuestionMapper.MapDaoToDomain).ToList()
            };
            return returned;
        }

        public static AdministratedTest MapDomainToDao(AdministratedTestBusiness domain)
        {
            var returned = new AdministratedTest
            {
                Id = domain.Id,
                URL = domain.URL,
                TestId = domain.TestId,
                Title = domain.Title,
                TestSubject = domain.TestSubject,
                Date = domain.Date,
                State = (byte) domain.State,
                AdministratedQuestions = domain.AdministratedQuestions.Select(AdministratedQuestionMapper.MapDomainToDao).ToList()
            };
            return returned;
        }

        public static AdministratedTestBusiness MapDaoToDomainHeavy(AdministratedTest dao)
        {
            var returned = new AdministratedTestBusiness
            {
                Id = dao.Id,
                URL = dao.URL,
                TestId = dao.TestId.Value,
                Title = dao.Title,
                TestSubject = dao.TestSubject,
                Date = dao.Date,
                AdministratedQuestions = dao.AdministratedQuestions.Select(AdministratedQuestionMapper.MapDaoToDomain).ToList()
            };

            return returned;
        }
    }
}
