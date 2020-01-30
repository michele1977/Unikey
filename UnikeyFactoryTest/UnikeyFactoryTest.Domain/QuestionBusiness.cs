using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class QuestionBusiness
    {
        public QuestionBusiness()
        {
            this.Answers = new List<AnswerBusiness>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
        public short Position { get; set; }

        public List<AnswerBusiness> Answers { get; set; }
        public TestBusiness Test { get; set; }
    }
}