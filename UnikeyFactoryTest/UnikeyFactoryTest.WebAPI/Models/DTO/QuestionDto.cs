using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;

namespace UnikeyFactoryTest.WebAPI.Models.DTO
{
    public class QuestionDto
    {
        public QuestionDto(QuestionBusiness question)
        {
            Id = question.Id;
            Answers = question.Answers.Select(a => new AnswerDto(a)).ToList();
            TestId = question.TestId;
            Text = question.Text;
            CorrectAnswerScore = question.Answers.Where(a => a.IsCorrect == AnswerState.Correct).Sum(a => a.Score);
            Position = question.Position;
        }
        public int CorrectAnswerScore { get; set; }
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public int Position { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}