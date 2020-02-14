using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Models
{
    public class ExTestModel
    {
        public int admTestId { get; set; }
        public string guid { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public AdministratedQuestionBusiness Question { get; set; }
    }
}