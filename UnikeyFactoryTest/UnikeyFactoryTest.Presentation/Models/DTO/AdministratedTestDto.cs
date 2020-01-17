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
            TestId = administratedTest.TestId;
            TestSubject = administratedTest.TestSubject;
            Date = administratedTest.Date;
            AdministratedQuestions =
                administratedTest.AdministratedQuestions.Select(q => new AdministratedQuestionDto(q)).ToList();
            ResultScore = administratedTest.Score;
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public string Text { get; set; }
        public int? TotalScore { get; set; }
        public int? TestId { get; set; }
        public string TestSubject { get; set; }
        public DateTime Date { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public decimal ResultScore { get; set; }
       
        public List<AdministratedQuestionDto> AdministratedQuestions { get; set; }
    }

}