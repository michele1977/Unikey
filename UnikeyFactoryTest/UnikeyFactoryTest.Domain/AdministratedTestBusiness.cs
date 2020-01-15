using System;
using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedTestBusiness
    {
        public AdministratedTestBusiness()
        {
            AdministratedQuestions = new List<AdministratedQuestionBusiness>();
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public int? TotalScore { get; set; }
        public int TestId { get; set; }
        public string TestSubject { get; set; }
        public DateTime? Date { get; set; }
        public decimal? ResultScore { get; set; }
        public virtual ICollection<AdministratedQuestionBusiness> AdministratedQuestions { get; set; }
    }
}
