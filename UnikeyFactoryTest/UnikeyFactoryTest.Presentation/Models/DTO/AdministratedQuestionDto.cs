using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class AdministratedQuestionDto
    {
        public AdministratedQuestionDto()
        {

        }

        public AdministratedQuestionDto(AdministratedQuestionBusiness administratedQuestion)
        {
            Id = administratedQuestion.Id;
            Text = administratedQuestion.Text;
            AdministratedAnswers = administratedQuestion.AdministratedAnswers.Select(a => new AdministratedAnswerDto(a)).ToList();

        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int AdministratedTestId { get; set; }
        public short Postition { get; set; }
        public List<AdministratedAnswerDto> AdministratedAnswers { get; set; }
    }
}