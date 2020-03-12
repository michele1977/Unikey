using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Models.DTO
{
    public class AdministratedTestDto
    {
        public AdministratedTestDto(AdministratedTestBusiness administratedTest)
        {
            Id = administratedTest.Id;
            URL = administratedTest.URL;
            TestId = administratedTest.TestId;
            TestSubject = administratedTest.TestSubject;
            Date = administratedTest.Date;
            AdministratedQuestions =
                administratedTest.AdministratedQuestions.Select(q => new AdministratedQuestionDto(q)).ToList();
            Score = administratedTest.Score;
            MaxScore = administratedTest.MaxScore;
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public int Score { get; set; }
        public int? TestId { get; set; }
        public string TestSubject { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfExTests { get; set; }
        public int MaxScore { get; set; }

        public ICollection<AdministratedQuestionDto> AdministratedQuestions { get; set; }
    }
}