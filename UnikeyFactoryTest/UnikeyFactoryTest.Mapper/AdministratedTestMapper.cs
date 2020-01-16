using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using State = UnikeyFactoryTest.Domain.State;

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
                TotalScore = dao.TotalScore,
                TestId = dao.TestId,
                TestSubject = dao.TestSubject,
                StateEnum = (State) dao.StateEnum
                
            };
            return returned;
        }

        public static AdministratedTestBusiness MapDaoToDomain(AdministratedTest dao)
        {
            var returned = new AdministratedTestBusiness
            {
                Id = dao.Id,
                URL = dao.URL,
                TotalScore = dao.TotalScore,
                TestId = dao.TestId,
                TestSubject = dao.TestSubject,
                Date = dao.Date,
                StateEnum = (State) dao.StateEnum, 
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
                TotalScore = domain.TotalScore,
                TestId = domain.TestId,
                TestSubject = domain.TestSubject,
                Date = domain.Date,
                StateEnum = (Context.State) domain.StateEnum,
                AdministratedQuestions = domain.AdministratedQuestions.Select(AdministratedQuestionMapper.MapDomainToDao).ToList()
            };
            return returned;
        }
    }
}
