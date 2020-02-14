using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class QuestionDto
    {
        public QuestionDto()
        {

        }

        public QuestionDto(QuestionBusiness question)
        {
            Id = question.Id;
            Answers = question.Answers.Select(a => new AnswerDto(a)).ToList();
            TestId = question.TestId;
            Text = question.Text;
            CorrectAnswerScore = question.Answers.Where(a => a.IsCorrect == AnswerState.Correct).Sum(a => a.Score);
            Position = question.Position;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public decimal CorrectAnswerScore { get; set; }

        public List<AnswerDto> Answers { get; set; }
        public short Position { get; set; }



        public QuestionBusiness MapToDomain()
        {
            var returned = new QuestionBusiness
            {
                Id = Id,
                Answers = new List<AnswerBusiness>(),
                Text = Text,
                TestId = TestId,
                Position = Position
                
                };
            foreach (var answerDto in this.Answers)
            {
                var answerBiz = new AnswerBusiness
                {
                    IsCorrect = answerDto.IsCorrect ? AnswerState.Correct : AnswerState.NotCorrect,
                    Score = answerDto.Score,
                    Text = answerDto.Text,
                    Id = answerDto.Id,
                    QuestionId = answerDto.QuestionId
                };
                returned.Answers.Add(answerBiz);
            }
            return returned;
        }
    }
}