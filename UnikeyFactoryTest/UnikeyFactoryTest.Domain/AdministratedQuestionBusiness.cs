using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedQuestionBusiness
    {
        public int Position { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int AdministratedTestId { get; set; }
        public virtual ICollection<AdministratedAnswerBusiness> AdministratedAnswers { get; set; }
    }
}
