using System;
using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedTestBusiness
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int? TotalScore { get; set; }
        public int TestId { get; set; }
        public string TestSubject { get; set; }
        public DateTime? Date { get; set; }
        public virtual ICollection<AdministratedQuestionBusiness> AdministratedQuestions { get; set; }
        public virtual TestBusiness Test { get; set; }
    }
}
