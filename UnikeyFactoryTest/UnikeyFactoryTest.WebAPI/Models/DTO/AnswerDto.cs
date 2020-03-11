using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;

namespace UnikeyFactoryTest.WebAPI.Models.DTO
{
    public class AnswerDto
    {
        public AnswerDto(AnswerBusiness answer)
        {
            Id = answer.Id;
            Text = answer.Text;
            IsCorrectBool = answer.IsCorrect == AnswerState.Correct ? true : false;
            QuestionId = answer.QuestionId;
            Score = answer.Score;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrectBool { get; set; }
        public int QuestionId { get; set; }
        public int Score { get; set; }
    }
}