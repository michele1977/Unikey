using System.Collections.Generic;
using System.Linq;
using ClassLibrary1.Business.Entities;

namespace ClassLibrary1.DAL.Mapper
{
    public class ExQuestionMapper
    {
        public static ExQuestion MapDalToEntity(DAO.ExTest_Question question)
        {
            var returned = new ExQuestion
            {
                Id = question.Id,
                QuestionId = question.QuestionId,
                Type = question.Question.Type,
                QuestionText = question.Question.QuestionText,
                Position = question.Position,
                ExPossibleAnswers = new List<ExPossibleAnswer>()
            };
            foreach (var pa in question.Question.PossibleAnswers)
            {
                returned.ExPossibleAnswers.Add(new ExPossibleAnswer()
                {
                    Id = pa.Id,

                    Text = pa.Text,
                    IsCorrect = pa.IsCorrect,
                    IsSelected = question.Pa_ExQuestion.First(x => x.PossibleAnswerId == pa.Id).IsSelected
                        
                });
            }
            return returned;
        }

        public static DAO.ExTest_Question MapEntityToDal(ExQuestion question)
        {
            var returned = new DAO.ExTest_Question
            {
                Id = question.Id,
                QuestionId = question.QuestionId,
                Position = question.Position,
                Pa_ExQuestion = question.ExPossibleAnswers
                    .Select(x => new DAO.Pa_ExQuestion()
                    {
                        PossibleAnswerId = x.Id,
                        IsSelected = x.IsSelected
                    }).ToList()
            };
            return returned;
        }
    }
}