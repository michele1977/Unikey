using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;

namespace UnikeyFactoryTest.webAPI_new.Models.DTO
{
    public class ExTestDto
    {
        private IAdministratedTestService _service;

        public ExTestDto(IAdministratedTestService service,
            AdministratedTestBusiness administratedTest)
        {
            _service = service;

            Id = administratedTest.Id;
            URL = administratedTest.URL;
            TestId = administratedTest.TestId;
            TestSubject = administratedTest.TestSubject;
            Date = administratedTest.Date;
            ExQuestions =
                administratedTest.AdministratedQuestions.Select(q => new ExQuestionDto(q)).ToList();
            Score = Task.Run(async () => await _service.GetScoreAndMax(Id)).Result.ElementAt(0).Value;
            MaxScore = Task.Run(async () => await _service.GetScoreAndMax(Id)).Result.ElementAt(1).Value;
            Title = administratedTest.Title;
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public int MaxScore { get; set; }
        public int? TestId { get; set; }
        public string TestSubject { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Timer { get; set; }
        public int TotalNumberOfExTests { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string TextFilter { get; set; }
        public int Score { get; set; }

        public List<ExQuestionDto> ExQuestions { get; set; }
    }
}