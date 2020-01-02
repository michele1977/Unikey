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
                AdministratedTestId = dao.AdministratedTestId
            };

            return returned;
        }

        public static Context.AdministratedQuestion MapDomainToDao(AdministratedQuestion domain)
        {
            var returned = new Context.AdministratedQuestion
            {
                Id = domain.Id,
                Text = domain.Text,
                AdministratedTestId = domain.AdministratedTestId
            };

            return returned;
        }
    }
}
