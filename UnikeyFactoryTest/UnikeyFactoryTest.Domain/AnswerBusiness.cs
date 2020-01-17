using UnikeyFactoryTest.Domain.Enums;

namespace UnikeyFactoryTest.Domain
{
    public class AnswerBusiness
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AnswerState IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public decimal Score { get; set; }
    }
}