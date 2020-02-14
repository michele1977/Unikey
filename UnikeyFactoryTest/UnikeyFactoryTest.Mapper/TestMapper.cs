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
        public static TestBusiness MapDalToBizLight(Test test)
        {
            var returned = new TestBusiness
            {
                Id = test.Id,
                URL = test.URL,
                Title = test.Title,
                Date = test.Date,
                UserId = test.UserId
            };
            return returned;
        }
        public static TestBusiness MapDalToBizHeavy(Test test)
        {
            var returned = new TestBusiness
            {
                Id = test.Id,
                URL = test.URL,
                Title = test.Title,
                Date = test.Date,
                Title = test.Title,
                UserId = test.UserId,
                Questions = test.Questions.Select(QuestionMapper.MapDalToBiz).ToList()
            };
            return returned;
        }
        public static Test MapBizToDal(TestBusiness test)
        {
            var returned = new Test
            {
                Id = test.Id,
                URL = test.URL,
                Title = test.Title,
                Date = test.Date,
                Title = test.Title,
                UserId = test.UserId,
                Questions = test.Questions.Select(QuestionMapper.MapBizToDal).ToList(),
            };
            return returned;
        }
    }
}
