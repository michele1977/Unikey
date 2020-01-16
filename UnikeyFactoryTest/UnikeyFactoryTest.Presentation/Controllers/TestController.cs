using System;
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
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;
using UnikeyFactoryTest.Service.Providers.MailProvider;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class TestController : Controller
    {
        private static int UserId { get; set; }
        private static readonly Test test = new Test();
        private readonly TestService _service = new TestService();

        // GET: Test
        public ActionResult Index(TestDto model)
        {
            if(UserId == 0)
                UserId = model.UserId;
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion(QuestionDto model)
        {
            var answerBiz = model.MapToDomain();
            var test = await _service.GetTestById(answerBiz.TestId);
            test.Questions.Add(answerBiz);
            _service.UpdateTest(test);
            return View("Index",model);
            //List<Answer> answers = new List<Answer>();
            //Answer correctAnswer = new Answer()
            //{
            //    Text = model.CorrectAnswerText,
            //    IsCorrect = true,
            //    Score = Convert.ToInt32(model.AnswerScore)
            //};
            //answers.Add(correctAnswer);
            //foreach (var AnswerText in model.Answers)
            //{
            //    if (!string.IsNullOrWhiteSpace(AnswerText))
            //    {
            //        Answer Answer = new Answer()
            //        {
            //            Text = AnswerText,
            //            IsCorrect = model.IsCorrect,
            //            Score = Convert.ToInt32(model.AnswerScore)
            //        };
            //        answers.Add(Answer);
            //    }
            //}

            //Question question = new Question()
            //{
            //    Text = model.QuestionText,
            //    Answers = answers
            //};
            //test.Questions.Add(question);
            //return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddTest(TestModel model)
        {
           // test.TestName = model.TestName;
            test.UserId = UserId;
            test.URL = _service.GenerateGuid();
            test.Date = model.Date;
             await _service.AddNewTest(TestMapper.MapDalToBizHeavy(test));
            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> TestsList(TestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new TestsListModel();

            TestService service = new TestService();

            testsListModel.Tests = testsListModel.Paginate(await service.GetTests());

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

        [HttpGet]
        public async Task<ActionResult> DeleteQuestion(QuestionDto question)
        {
            TestService service = new TestService();
            await service.DeleteQuestion(question.Id);

            return View("Index");
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
        public async Task<JsonResult> SendMail(EmailModel emailModel)
        {
            TestService service = new TestService();
            var test = await service.GetTestById(emailModel.Id);
            TestDto sendTest = new TestDto(test);
            var URL = sendTest.URL;
            MailProvider provider = new MailProvider();
            var result = provider.SendMail(emailModel.email, emailModel.name, URL);
            return Json(new {result = result});
        }
    }
}
