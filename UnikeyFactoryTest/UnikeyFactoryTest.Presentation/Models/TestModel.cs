﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Presentation.Models.Dto;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class TestModel
    {
        public TestModel()
        {

        }

        public TestModel(QuestionDto question)
        {
            TestService service = new TestService();
            Test = new TestDto(service.GetTestById(question.TestId));
            QuestionText = question.Text;

            AnswerDto correctAnswer = question.Answers.FirstOrDefault(a => (bool) a.IsCorrect);
            CorrectAnswerText = correctAnswer.Text;
            AnswerScore = correctAnswer.Score.ToString();
            WrongAnswers = question.Answers.Where(a => !(bool)a.IsCorrect).Select(a => a.Text).ToList();

            PageNumber = question.PageNumber;
            PageSize = question.PageSize;
            AnswerScore = question.CorrectAnswerScore.ToString();
        }

        public TestDto Test { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswerText { get; set; }
        public List<string> WrongAnswers { get; set; }
        public int UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string AnswerScore { get; set; }
    }
}