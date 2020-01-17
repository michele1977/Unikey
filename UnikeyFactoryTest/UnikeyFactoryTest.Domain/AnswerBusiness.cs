namespace UnikeyFactoryTest.Domain
{
    public class AnswerBusiness
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public decimal Score { get; set; }
        public QuestionBusiness Question { get; set; }
    }
}