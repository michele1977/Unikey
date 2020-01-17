using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class AnswerDto
    {
        public AnswerDto()
        {

        }

        public AnswerDto(AnswerBusiness answer)
        {
            Id = answer.Id;
            Text = answer.Text;
            IsCorrect = answer.IsCorrect;
            QuestionId = answer.QuestionId;
            Score = answer.Score;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public byte IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public decimal Score { get; set; }
    }
}