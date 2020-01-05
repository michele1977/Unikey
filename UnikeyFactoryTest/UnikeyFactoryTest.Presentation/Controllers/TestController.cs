using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class TestController : Controller
    {
        private static readonly Test test = new Test();
        private readonly TestService _service = new TestService();

        // GET: Test
        public ActionResult Index()
        {
            TestModel model = new TestModel();
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddQuestion(TestModel model)
        {
            List<Answer> answers = new List<Answer>();
            Answer correctAnswer = new Answer()
            {
                Text = model.CorrectAnswerText,
                IsCorrect = true,
                Score = Convert.ToInt32(model.AnswerScore)
            };
            answers.Add(correctAnswer);
            foreach (var wrongAnswerText in model.WrongAnswers)
            {
                if (!string.IsNullOrWhiteSpace(wrongAnswerText))
                {
                    Answer wrongAnswer = new Answer()
                    {
                        Text = wrongAnswerText,
                        IsCorrect = false
                    };
                    answers.Add(wrongAnswer);
                }
            }

            Question question = new Question()
            {
                Text = model.QuestionText,
                Answers = answers
            };
            test.Questions.Add(question);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddTest(TestModel model)
        {
            test.URL = _service.GenerateUrl();
            test.Date = DateTime.Now;
            _service.AddNewTest(test);
            return View("Index");
        }

    }
}