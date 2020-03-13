using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.webAPI_new.Models.DTO
{
    public class ExAnswerDto
    {
        public ExAnswerDto(AdministratedAnswerBusiness administratedAnswer)
        {
            Id = administratedAnswer.Id;
            Text = administratedAnswer.Text;
            ExQuestionId = administratedAnswer.AdministratedQuestionId;
            IsCorrect = (byte)administratedAnswer.isCorrect;
            IsSelected = administratedAnswer.isSelected;
            Score = administratedAnswer.Score;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public byte IsCorrect { get; set; }
        public bool IsSelected { get; set; }
        public int ExQuestionId { get; set; }
        public int Score { get; set; }
    }
}