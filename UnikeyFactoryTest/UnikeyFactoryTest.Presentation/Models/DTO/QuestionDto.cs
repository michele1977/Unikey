using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using UnikeyFactoryTest.Domain;

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
            CorrectAnswerScore = question.Answers.Where(a => (bool) a.IsCorrect).Sum(a => a.Score);
        }



        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public decimal? CorrectAnswerScore { get; set; }

        public List<AnswerDto> Answers { get; set; }




        public QuestionBusiness MapToDomain()
        {
            var returned = new QuestionBusiness
            {
                Id = Id,
                Answers = new List<AnswerBusiness>()

            };
            foreach (var answerDto in this.Answers)
            {
                var answerBiz = new AnswerBusiness
                {
                    IsCorrect = answerDto.IsCorrect,
                    Score = answerDto.Score
                };
                returned.Answers.Add(answerBiz);
            }
            return returned;
        }
    }
}