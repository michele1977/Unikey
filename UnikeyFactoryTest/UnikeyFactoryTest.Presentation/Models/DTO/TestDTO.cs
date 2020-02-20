using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class TestDto
    {
        private IAdministratedTestService service;
        private ITestService testService;
        public TestDto()
        {
            Questions = new List<QuestionDto>();
        }
        public TestDto(IAdministratedTestService value)
        {
            Questions = new List<QuestionDto>();
            service = value;
        }

        public TestDto(TestBusiness test, ITestService value)
        {
            testService = value;

            Id = test.Id;
            Title = test.Title;
            URL = testService.GenerateUrl(test.URL);
            Date = test.Date;
            UserId = test.UserId;
            Title = test.Title;
            Questions = test.Questions?.Select(q => new QuestionDto(q)).ToList();
            AdministratedTests = new List<AdministratedTestDto>();

        }

        #region FillAdministratedTests()

        public async Task FillAdministratedTests(int testId)
        {
            var filteredList = (await GetAdministratedTestsByTestId(testId)).Where(t => t.TestId == Id);
            var dtoList = filteredList.Select(adTest => new AdministratedTestDto(adTest)).ToList();

            AdministratedTests = dtoList;
        }

        private async Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int testId)
        {
            var listAdTest = await service.GetAdministratedTestsByTestId(testId);
            return listAdTest;
        }

        #endregion)
        

        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public int UserId { get; set; }
        public int NumberOfTest { get; set; }
        public bool ShowForm { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string TextFilter { get; set; }
        public int NumQuestions { get; set; }

        public decimal? MaxScore
        {
            get => Questions.Sum(q => q.CorrectAnswerScore);
        }

        public List<AdministratedTestDto> AdministratedTests { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }
}