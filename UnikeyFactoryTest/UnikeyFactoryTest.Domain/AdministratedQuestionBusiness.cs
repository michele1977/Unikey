using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedQuestionBusiness
    {
        public AdministratedQuestionBusiness()
        {
            this.AdministratedAnswers = new List<AdministratedAnswerBusiness>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int AdministratedTestId { get; set; }
        public short Position { get; set; }
        public  List<AdministratedAnswerBusiness> AdministratedAnswers { get; set; }
    }
}
