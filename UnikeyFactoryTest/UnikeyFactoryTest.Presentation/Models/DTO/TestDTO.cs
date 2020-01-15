using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Models.Dto
{
    public class TestDto
    {
        public TestDto()
        {
            Questions = new List<QuestionDto>();
        }
        
        public TestDto(TestBusiness test)
        {
            var service = new TestService();

            Id = test.Id;
            URL = service.GenerateUrl(test.URL);
            Date = test.Date;
            UserId = test.UserId;
            Questions = test.Questions?.Select(q => new QuestionDto(q)).ToList();
            AdministratedTests = new List<AdministratedTestDto>();
            NumQuestions = test.NumQuestions;
            
        }

        public async Task FillAdministratedTests(AdministratedTestService business)
        {
            var dtoList = new List<AdministratedTestDto>();
            var listAdministratedTest = await business.GetAdministratedTests();

            foreach (var adTest in listAdministratedTest)
            {
                dtoList.Add(new AdministratedTestDto(adTest));
            }

            AdministratedTests = dtoList;
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int NumQuestions { get; set; }

        public decimal? MaxScore
        {
            get =>
                Questions.Sum(q => q.CorrectAnswerScore);
        }

        //public decimal? CalculateScore()
        //{
        //    try
        //    {
        //        return Questions.SelectMany(q => q.Answers).Sum(a => a.Score);
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        return null;
        //    }
        //    catch (OverflowException ex)
        //    {
        //        return null;
        //    }
        //}

        public List<AdministratedTestDto> AdministratedTests { get; set; }
        //public List<AdministratedTestDto> AdministratedTestsOpened { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }
}