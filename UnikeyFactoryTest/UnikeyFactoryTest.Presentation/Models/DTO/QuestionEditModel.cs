using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class QuestionEditModel
    {
        public QuestionEditModel()
        {

        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public decimal CorrectAnswerScore { get; set; }
        public List<string> Answers { get; set; }
        public short Position { get; set; }
    }
}