using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1.Business.Entities;

namespace ClassLibrary1.Business
{
    public class TestExFactory
    {
        public ExTest Create(Test test,string name)
        {
            var returned = new ExTest {
                State = 0, 
                TestId = test.Id,
                Name = name,
                ExecutionDate = DateTime.Today, 
                ExQuestions = new List<ExQuestion>()};
            foreach (var question in test.Questions)
            {
                returned.ExQuestions.Add(new ExQuestion()
                {
                    
                    QuestionId = question.Id,
                    Type = question.Type,
                    QuestionText = question.QuestionText,
                    ExPossibleAnswers = question.PossibleAnswers.Select(x => new ExPossibleAnswer() {Id = x.Id, IsCorrect = x.IsCorrect, Text = x.Text}).ToList()
                });
            }
                
            return returned;
        }
    }
}
