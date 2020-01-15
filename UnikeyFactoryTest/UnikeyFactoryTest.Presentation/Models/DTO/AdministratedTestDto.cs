using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class AdministratedTestDto
    {
        public AdministratedTestDto()
        {

        }
        public AdministratedTestDto(AdministratedTestBusiness administratedTest)
        {
            Id = administratedTest.Id;
            URL = administratedTest.URL;
            TotalScore = administratedTest.TotalScore;
            TestId = administratedTest.TestId;
            TestSubject = administratedTest.TestSubject;
            Date = administratedTest.Date;
            AdministratedQuestions =
                administratedTest.AdministratedQuestions.Select(q => new AdministratedQuestionDto(q));
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public string Text { get; set; }
        public int? TotalScore { get; set; }
        public int TestId { get; set; }
        public string TestSubject { get; set; }
        public DateTime? Date { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public enum State
        {
            Created = 1, Open , Started , Closed 
        }
        public decimal? CalculateMaxScore()
        {
            try
            {
                return AdministratedQuestions.SelectMany(q => q.AdministratedAnswers).Sum(a => a.Score);
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
            catch (OverflowException ex)
            {
                return null;
            }
        }

        public decimal? CalculateResultScore()
        {
            try
            {
                return AdministratedQuestions.SelectMany(q => q.AdministratedAnswers).Where(a => (bool) a.IsCorrect && (bool)a.IsSelected).Sum(a => a.Score);
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
            catch (OverflowException ex)
            {
                return null;
            }
        }

        public IEnumerable<AdministratedQuestionDto> AdministratedQuestions { get; set; }
    }
}