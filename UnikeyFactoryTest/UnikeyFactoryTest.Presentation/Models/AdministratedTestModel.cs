using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class AdministratedTestModel
    {
        public AdministratedTest Test { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}