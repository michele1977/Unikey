using System;
using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class TestBusiness
    {
        public TestBusiness()
        {
            AdministratedTests = new List<AdministratedTestBusiness>();
            Questions = new List<QuestionBusiness>();
        }
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public List<AdministratedTestBusiness> AdministratedTests { get; set; }
        public List<QuestionBusiness> Questions { get; set; }
    }
}