using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class AdministratedAnswerDto
    {
        public AdministratedAnswerDto()
        {

        }

        public AdministratedAnswerDto(AdministratedAnswerBusiness administratedAnswer)
        {
            Id = administratedAnswer.Id;
            Text = administratedAnswer.Text;
            AdministratedQuestionId = administratedAnswer.AdministratedQuestionId;
            IsCorrect = (byte)administratedAnswer.isCorrect;
            IsSelected = administratedAnswer.isSelected;
            Score = administratedAnswer.Score;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public byte IsCorrect { get; set; }
        public bool IsSelected { get; set; }
        public int AdministratedQuestionId { get; set; }
        public decimal Score { get; set; }
    }
}