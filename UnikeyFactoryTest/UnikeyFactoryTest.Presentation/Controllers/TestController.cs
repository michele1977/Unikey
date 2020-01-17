using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static int UserId { get; set; }
        private static readonly Test test = new Test();
        private readonly TestService _service = new TestService();

        // GET: Test
        public ActionResult Index(TestDto model)
        {
            if (UserId == 0)
                UserId = model.UserId;
            try
            {
                ModelState.Clear();
            }
            catch (NotSupportedException e)
            {
                Logger.Warn(e, e.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion(QuestionDto model)
        {
            try
            {
                var answerBiz = model.MapToDomain();
                var test = await _service.GetTestById(answerBiz.TestId);
                test.Questions.Add(answerBiz);
                _service.UpdateTest(test);
            }
            catch (ArgumentNullException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (InvalidOperationException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbEntityValidationException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (NotSupportedException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }

            return View("Index");
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
            try
            {
                // test.TestName = model.TestName;
                test.UserId = UserId;
                test.URL = _service.GenerateGuid();
                test.Date = model.Date;
                await _service.AddNewTest(TestMapper.MapDalToBizHeavy(test));
            }
            catch (ArgumentNullException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (InvalidOperationException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbEntityValidationException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (NotSupportedException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> TestsList(TestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new TestsListModel();

            TestService service = new TestService();

            try
            {
                testsListModel.Tests = testsListModel.Paginate(await service.GetTests());
            }
            catch (ArgumentNullException e)
            {
                Logger.Warn(e, e.Message);
            }
            catch (Exception e)
            {
                Logger.Fatal(e, e.Message);
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
                throw new ArgumentNullException();
                await service.DeleteTest(test.Id);
                Logger.Info("Successfully deleted test");
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, e.Message);
                return Json(new { redirectUrl = Url.Action("Index", "Error") });
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e, e.Message);
                return Json(new { redirectUrl = Url.Action("Index", "Error") });
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Fatal(e, e.Message);
                return Json(new { redirectUrl = Url.Action("Index", "Error") });
            }
            catch (DbEntityValidationException e)
            {
                Logger.Fatal(e, e.Message);
                return Json(new { redirectUrl = Url.Action("Index", "Error") });
            }
            catch (DbUpdateException e)
            {
                Logger.Fatal(e, e.Message);
                return Json(new { redirectUrl = Url.Action("Index", "Error") });
            }
            catch (NotSupportedException e)
            {
                Logger.Error(e, e.Message);
                return Json(new { redirectUrl = Url.Action("Index", "Error") });
            }
            catch (Exception e)
            {
                Logger.Fatal(e, e.Message);
                return Json(new { redirectUrl = Url.Action("Index", "Error") });
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
                Logger.Error(ex, ex.Message);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }

            return View(testToPass);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteQuestion(QuestionDto question)
        {
            try
            {
                TestService service = new TestService();
                await service.DeleteQuestion(question.Id);
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbEntityValidationException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (NotSupportedException e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }

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
            TestBusiness test;

            try
            {
                TestService service = new TestService();
                test = await service.GetTestById(emailModel.Id);
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbEntityValidationException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }
            catch (NotSupportedException e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                Logger.Fatal(e, e.Message);
                throw;
            }

            bool result = false;
            TestDto sendTest = new TestDto(test);
            var URL = sendTest.URL;
            MailProvider provider = new MailProvider();

            try
            {
                result = provider.SendMail(emailModel.email, emailModel.name, URL);
            }
            catch (HttpException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, e.Message);

            }
            catch (ArgumentOutOfRangeException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (ArgumentException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (FormatException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (ObjectDisposedException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (SmtpFailedRecipientException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (SmtpException e)
            {
                Logger.Error(e, e.Message);
            }
            catch (Exception e)
            {
                Logger.Fatal(e, e.Message);
            }

            return Json(new { result = result });
        }
    }
}
