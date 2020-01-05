using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class TestModel
    {
        public Test Test { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswerText { get; set; }
        public List<string> WrongAnswers { get; set; }

        public string AnswerScore { get; set; }
    }
}