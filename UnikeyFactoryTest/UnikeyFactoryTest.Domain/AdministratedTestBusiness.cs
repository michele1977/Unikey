using System;
using System.Collections.Generic;
using UnikeyFactoryTest.Domain.Enums;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedTestBusiness
    {
        public AdministratedTestBusiness()
        {
            this.AdministratedQuestions = new List<AdministratedQuestionBusiness>();
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public int? TestId { get; set; }
        public string TestSubject { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal MaxScore { get; set; }
        public AdministratedTestState State { get; set; }
        public decimal Score { get; set; }
        public virtual List<AdministratedQuestionBusiness> AdministratedQuestions { get; set; }
        public TestBusiness Test { get; set; }
    }
}
