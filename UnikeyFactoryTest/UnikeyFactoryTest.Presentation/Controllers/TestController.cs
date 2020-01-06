using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Presentation.Models.Dto;
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
            _service.AddNewTest(TestMapper.MapDalToBiz(test));
            return View("Index");
        }

        [HttpGet]
        public ActionResult TestsList(TestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new TestsListModel();

            TestService service = new TestService();

            testsListModel.Tests = testsListModel.Paginate(service.GetTests().ToList());

            if (testsListModel.IsAjaxCall)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("TestsList",
                        new { PageNumber = testsListModel.PageNumber, PageSize = testsListModel.PageSize })
                });
            }

            return View(testsListModel);
        }

        [HttpPost]
        public JsonResult DeleteTest(TestDto test)
        {
            TestService service = new TestService();
            service.DeleteTest(test.Id);

            return Json(new
            {
                redirectUrl = Url.Action("TestsList",
                    new { PageNumber = test.PageNumber, PageSize = test.PageSize })
            });
        }

        [HttpGet]
        public ActionResult TestContent(TestDto test)
        {
            TestService service = new TestService();
            TestDto testToPass = new TestDto(service.GetTestById(test.Id));
            testToPass.PageNumber = test.PageNumber;
            testToPass.PageSize = test.PageSize;
            return View(testToPass);
        }

        [HttpPost]
        public ActionResult TextSearch(TestsListModel testsListModel)
        {
            if (!testsListModel.TextFilter.IsNullOrWhiteSpace())
            {
                TestService service = new TestService();

                testsListModel.Tests = service.GetTests()
                    .Where(t => t.User.Username.Contains(testsListModel.TextFilter))
                    .Select(t => new TestDto(t)).ToList();
            }

            return RedirectToAction("TestsList");
        }
    }
}
