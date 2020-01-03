using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public class AdministratedTestMapper
    {
        public static AdministratedTest MapDaoToDomain(Context.AdministratedTest dao)
        {
            var returned = new AdministratedTest
            {
                Id = dao.Id,
                URL = dao.URL,
                Text = dao.Text,
                TotalScore = dao.TotalScore,
                TestId = dao.TestId,
                TestSubject = dao.TestSubject,
                Date = dao.Date,
                AdministratedQuestions = new List<AdministratedQuestion>()
            };

            foreach(var aq in dao.AdministratedQuestions)
            {
                returned.AdministratedQuestions.Add(new AdministratedQuestion
                {
                    Id = aq.Id,
                    Text = aq.Text,
                    AdministratedTestId = returned.Id
                });
            }

            return returned;
        }

        public static Context.AdministratedTest MapDomainToDao(AdministratedTest domain)
        {
            var returned = new Context.AdministratedTest
            {
                Id = domain.Id,
                URL = domain.URL,
                Text = domain.Text,
                TotalScore = domain.TotalScore,
                TestId = domain.TestId,
                TestSubject = domain.TestSubject,
                Date = domain.Date,
                AdministratedQuestions = new List<Context.AdministratedQuestion>()
            };

            foreach(var aq in domain.AdministratedQuestions)
            {
                returned.AdministratedQuestions.Add(new Context.AdministratedQuestion
                {
                    Id = aq.Id,
                    Text = aq.Text,
                    AdministratedTestId = returned.Id
                });
            }

            return returned;
        }

    }
}
