using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Models.DTO
{
    public class AdministratedQuestionDto
    {
        public AdministratedQuestionDto(AdministratedQuestionBusiness administratedQuestion)
        {
            Id = administratedQuestion.Id;
            Text = administratedQuestion.Text;
            Position = administratedQuestion.Position;
            AdministratedTestId = administratedQuestion.AdministratedTestId;
            AdministratedAnswers = administratedQuestion.AdministratedAnswers.Select(a => new AdministratedAnswerDto(a)).ToList();

        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int AdministratedTestId { get; set; }
        public short Position { get; set; }
        public ICollection<AdministratedAnswerDto> AdministratedAnswers { get; set; }
    }
}