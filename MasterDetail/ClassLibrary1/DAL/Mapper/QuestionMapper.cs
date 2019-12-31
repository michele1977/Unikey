using System.Linq;
using ClassLibrary1.Business.Entities;

namespace ClassLibrary1.DAL.Mapper
{
    public class QuestionMapper
    {
        public static Question MapDalToEntity(DAL.DAO.Question question)
        {
            var returned = new Question
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                State = question.State,
                Type = question.Type,
                PossibleAnswers = question.PossibleAnswers.Select(PossibleAnswerMapper.MapDalToEntity).ToList()
            };
            return returned;
        }

        public static DAL.DAO.Question MapEntityToDal(Question question)
        {
            var returned = new DAL.DAO.Question
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                State = question.State,
                Type = question.Type,
                PossibleAnswers = question.PossibleAnswers.Select(PossibleAnswerMapper.MapEntityToDal).ToList()
            };

            return returned; 
        }
    }
}
