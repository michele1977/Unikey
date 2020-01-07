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
        public Dictionary<int,int> QuestionAnswerDictionary { get; set; }

        public AdministratedTestBusiness Test { get; set; }
        public int admnistratedTestId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}