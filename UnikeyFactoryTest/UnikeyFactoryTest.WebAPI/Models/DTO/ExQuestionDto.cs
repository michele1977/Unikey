using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Models.DTO
{
    public class ExQuestionDto
    {
        public ExQuestionDto(AdministratedQuestionBusiness administratedQuestion)
        {
            Id = administratedQuestion.Id;
            Text = administratedQuestion.Text;
            ExAnswers = administratedQuestion.AdministratedAnswers.Select(a => new ExAnswerDto(a)).ToList();
            ExTestId = administratedQuestion.AdministratedTestId;
            Position = administratedQuestion.Position;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int ExTestId { get; set; }
        public short Position { get; set; }
        public List<ExAnswerDto> ExAnswers { get; set; }
    }
}