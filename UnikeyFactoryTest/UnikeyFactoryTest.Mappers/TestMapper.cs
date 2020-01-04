using System.Linq;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mappers
{
    public static class TestMapper
    {
        public static TestBusiness MapDalToBiz(Test test)
        {
            var returned = new TestBusiness
            {
                Id = test.Id,
                URL = test.URL,
                Date = test.Date,
                UserId = test.UserId,
                AdministratedTests = test.AdministratedTests.Select(AdministratedTestMapper.MapDalToBiz),
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
                AdministratedTests = test.AdministratedTests.Select(AdministratedTestMapper.MapBizToDal).ToList(),
                Questions = test.Questions.Select(QuestionMapper.MapBizToDal).ToList(),
            };
            return returned;
        }
    }
}
