using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class AdministratedTestModel
    {
        public Dictionary<int, int> QuestionAnswerDictionary { get; set; }

        public int NumQuestion { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Url { get; set; }
        public int ActualPosition { get; set; }
        public int AdministratedTestId { get; set; }
        public AdministratedQuestionBusiness ActualQuestion { get; set; }
    }
}