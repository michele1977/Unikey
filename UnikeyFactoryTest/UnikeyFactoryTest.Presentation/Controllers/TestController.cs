using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static int UserId { get; set; }
        private static readonly Test test = new Test();
        private readonly TestService _service = new TestService();

        // GET: Test
        public ActionResult Index(TestModel model)
        {
            if(UserId == 0)
                UserId = model.UserId;

            try
            {
                ModelState.Clear();
            }
            catch (NotSupportedException e)
            {
                Logger.Warn(e);
            }

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
            };

            try
            {
                correctAnswer.Score = Convert.ToInt32(model.AnswerScore);
            }
            catch (FormatException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (InvalidCastException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (OverflowException e)
            {
                Logger.Error(e);
                throw;
            }
            
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

            try
            {
                test.Questions.Add(question);
            }
            catch (NotSupportedException e)
            {
                Logger.Error(e);
                throw;
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddTest(TestModel model)
        {
            test.UserId = UserId;
            test.URL = _service.GenerateGuid();
            test.Date = DateTime.Now;

            try
            {
                await _service.AddNewTest(TestMapper.MapDalToBizHeavy(test));
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (DbEntityValidationException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (NotSupportedException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> TestsList(TestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new TestsListModel();

            var service = new TestService();

            try
            {
                var tests = await service.GetTests();
                testsListModel.Tests = testsListModel.Paginate(tests);

                foreach (var dto in testsListModel.Tests)
                {
                    await dto.FillAdministratedTests(dto.Id);
                
                }

            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e);
                throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
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
            
            try
            {
                await service.DeleteTest(test.Id);
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e);
                return Json(new {redirectUrl = Url.Action("Index", "Error")});
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e);
                return Json(new {redirectUrl = Url.Action("Index", "Error")});
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Error(e);
                return Json(new {redirectUrl = Url.Action("Index", "Error")});
            }
            catch (DbEntityValidationException e)
            {
                Logger.Error(e);
                return Json(new {redirectUrl = Url.Action("Index", "Error")});
            }
            catch (DbUpdateException e)
            {
                Logger.Error(e);
                return Json(new {redirectUrl = Url.Action("Index", "Error")});
            }
            catch (NotSupportedException e)
            {
                Logger.Error(e);
                return Json(new {redirectUrl = Url.Action("Index", "Error")});
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Json(new {redirectUrl = Url.Action("Index", "Error")});
            }

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
            TestDto testToPass = new TestDto();

            try
            {
                testToPass = new TestDto(await service.GetTestById(test.Id));
                testToPass.PageNumber = test.PageNumber;
                testToPass.PageSize = test.PageSize;
                testToPass.URL = service.GenerateUrl(testToPass.URL);
            }
            catch (ArgumentNullException ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

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
