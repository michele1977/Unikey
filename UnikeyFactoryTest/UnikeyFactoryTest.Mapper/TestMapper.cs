using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public class TestMapper
    {
        public static TestBusiness MapDalToBiz(Test test)
        {
            var returned = new TestBusiness
            {
                Id = test.Id,
                URL = test.URL,
                Date = test.Date,
                UserId = test.UserId,
                AdministratedTests = test.AdministratedTests.Select(AdministratedTestMapper.MapDaoToDomain),
                Questions = test.Questions.Select(QuestionMapper.MapDalToBiz).ToList(),
        };
            return returned;
        }

        public static Test MapBizToDal(TestBusiness test)
        {
            var returned = new Test
            {
                Id = test.Id,
                URL = test.URL,
                Date = test.Date,
                UserId = test.UserId,
                AdministratedTests = test.AdministratedTests.Select(AdministratedTestMapper.MapDomainToDao).ToList(),
                Questions = test.Questions.Select(QuestionMapper.MapBizToDal).ToList(),
            };
            return returned;
        }
    }
}
