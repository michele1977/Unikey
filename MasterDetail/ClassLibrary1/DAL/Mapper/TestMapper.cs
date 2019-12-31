using System.Linq;
using ClassLibrary1.Business.Entities;

namespace ClassLibrary1.DAL.Mapper
{
    public static class TestMapper
    {
        public static Test MapDalToEntity(DAL.DAO.Test test)
        {
            var returned = new Test
            {
                Id = test.Id,
                State = test.State,
                CreationDate = test.CreationDate,
                Title = test.Title,
                Questions = test.Questions.Select(QuestionMapper.MapDalToEntity).ToList()
            };
            return returned;
        }

        public static DAL.DAO.Test MapEntityToDal(Test test)
        {
            var returned = new DAL.DAO.Test
            {
                Id = test.Id,
                State = test.State,
                CreationDate = test.CreationDate,
                Title = test.Title,
                Questions = test.Questions.Select(QuestionMapper.MapEntityToDal).ToList()
            };
            return returned;
        }
    }
}
