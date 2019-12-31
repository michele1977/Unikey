using System.Linq;
using ClassLibrary1.Business.Entities;

namespace ClassLibrary1.DAL.Mapper
{
    public static class TestExMapper
    {
        public static ExTest MapDalToEntity(DAL.DAO.ExTest exTest)
        {
            var returned = new ExTest
            {
                Id = exTest.Id,
                State = exTest.State,
                TestId = exTest.TestId,
                ExecutionDate = exTest.ExecutionDate,
                Name = exTest.Name,
                ExQuestions = exTest.ExTest_Question.Select(ExQuestionMapper.MapDalToEntity).ToList()
            };
            return returned;
        }

        public static DAL.DAO.ExTest MapEntityToDal(ExTest exTest)
        {
            var returned = new DAL.DAO.ExTest
            {
                Id = exTest.Id,
                TestId = exTest.TestId,
                State = exTest.State,
                ExecutionDate = exTest.ExecutionDate,
                Name = exTest.Name,
                ExTest_Question = exTest.ExQuestions.Select(ExQuestionMapper.MapEntityToDal).ToList()
            };
            return returned;
        }
    }
}