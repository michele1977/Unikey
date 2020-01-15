﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Presentation.Models.Dto;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class TestController : Controller
    {
        private static int UserId { get; set; }
        private static readonly Test test = new Test();
        private readonly TestService _service = new TestService();

        // GET: Test
        public ActionResult Index(TestModel model)
        {
            if(UserId == 0)
                UserId = model.UserId;
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
        public async Task<ActionResult> AddTest(TestModel model)
        {
            test.UserId = UserId;
            test.URL = _service.GenerateGuid();
            test.Date = DateTime.Now;
            await _service.AddNewTest(TestMapper.MapDalToBizHeavy(test));
            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> TestsList(TestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new TestsListModel();

            var service = new TestService();
            var tests = await service.GetTests();
            testsListModel.Tests = testsListModel.Paginate(tests);

            var adTestService = new AdministratedTestService();
            foreach (var dto in testsListModel.Tests)
            {
                await dto.FillAdministratedTests(adTestService);
                
            }

            return View(testsListModel);
        }

        //[HttpGet]
        //public async Task<ActionResult> TestsListJson(TestsListModel testsListModel)
        //{
        //    testsListModel = testsListModel ?? new TestsListModel();

        //    TestService service = new TestService();

        //    testsListModel.Tests = testsListModel.Paginate(await service.GetTests());

        //    return Json(new
        //    {
        //        redirectUrl = Url.Action("TestsList",
        //            new { PageNumber = testsListModel.PageNumber, PageSize = testsListModel.PageSize })
        //    }, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public async Task<JsonResult> DeleteTest(TestDto test)
        {
            TestService service = new TestService();
            await service.DeleteTest(test.Id);

            return Json(new
            {
                redirectUrl = Url.Action("TestsList",
                    new { PageNumber = test.PageNumber, PageSize = test.PageSize })
            });
        }

        [HttpGet]
        public async Task<ActionResult> TestContent(TestDto test)
        {
            TestService service = new TestService();
            TestDto testToPass = new TestDto(await service.GetTestById(test.Id));
            testToPass.PageNumber = test.PageNumber;
            testToPass.PageSize = test.PageSize;
            testToPass.URL = service.GenerateUrl(testToPass.URL);
            return View(testToPass);
        }

        //[HttpPost]
        //public ActionResult TextSearch(TestsListModel testsListModel)
        //{
        //    if (!testsListModel.TextFilter.IsNullOrWhiteSpace())
        //    {
        //        TestService service = new TestService();

        //        testsListModel.Tests = service.GetTests()
        //            .Where(t => t.User.Username.Contains(testsListModel.TextFilter))
        //            .Select(t => new TestDto(t)).ToList();
        //    }

        //    return RedirectToAction("TestsList");
        //}

        //[HttpGet]
        //[ActionName("EditQuestion")]
        //public ActionResult EditQuestion_Get(QuestionDto question)
        //{
        //    TestBusiness questionRelatedTest = _service.GetTestById(question.TestId);

        //    QuestionDto questionToEdit = new QuestionDto(questionRelatedTest.Questions.FirstOrDefault(q => q.Id == question.Id));

        //    questionToEdit.CorrectAnswerScore = question.CorrectAnswerScore;

        //    TestModel questionToUpdate = new TestModel(questionToEdit);

        //    return View(questionToUpdate);
        //}

        //[HttpPost]
        //[ActionName("EditQuestion")]
        //public ActionResult EditQuestion_Post(TestModel question)
        //{
        //    // TODO
            
        //    return RedirectToAction("TestContent", "Test", new {Id = question.Test.Id});
        //}
    }
}
