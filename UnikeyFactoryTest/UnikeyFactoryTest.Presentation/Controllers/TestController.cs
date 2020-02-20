using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Domain.Enums;
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

        //private readonly TestService _service = new TestService();
        //private ITestService service;
        private static int UserId { get; set; }
        private static readonly Test test = new Test();
        private IAdministratedTestService administratedservice;
        private ITestService _service;

        public TestController()
        {

        }

        public TestController(ITestService value, IAdministratedTestService value2)
        {
            _service = value;
            administratedservice = value2;
        }

        // GET: Test
        public ActionResult Index(TestDto model)
        {
            if ((TestDto)TempData["mod"] != null)
                model = (TestDto)TempData["mod"];
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

            model.ShowForm = false;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion(QuestionDto model)
        {
            var returned = new TestDto(administratedservice);
            try
            {
                var questionBiz = model.MapToDomain();
                var test = await _service.GetTestById(questionBiz.TestId);
                if(test.Questions.Count == 0)
                {
                    questionBiz.Position = 0;
                }
                else
                {
                    questionBiz.Position = Convert.ToInt16(test.Questions.Count );
                }
                test.Questions.Add(questionBiz);

               _service.UpdateTest(test);
              
                returned = new TestDto(await _service.GetTestById(test.Id), _service);
             

              
                //returned = new TestDto(await _service.GetTestById(test.Id));
                //returned.ShowForm = true;
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

            TempData["mod"] = returned;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddTest(TestDto model)
        {
            try
            {
                test.Id = model.Id;
                test.Title = model.Title;
                test.UserId = UserId;
                test.URL = _service.GenerateGuid();
                test.Date = model.Date;
                var testDomain = TestMapper.MapDalToBizHeavy(test);
                _service.AddNewTest(testDomain);
                model.Id = testDomain.Id;

                model.ShowForm = true;
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

            return View("index", model);
        }

        [HttpGet]
        public async Task<ActionResult> TestsList(TestsListModel testsListModel)
        {
            UserId = System.Convert.ToInt32(HttpContext.Session["UserId"]);
            testsListModel = testsListModel ?? new TestsListModel(_service);

            try
            {
                List<TestBusiness> tests = new List<TestBusiness>();

                tests = testsListModel.TextFilter.IsNullOrWhiteSpace() ? await _service.GetTests() :
                    await _service.GetTestsByFilter(testsListModel.TextFilter);

                testsListModel.Tests = testsListModel.Paginate(tests);

                testsListModel.ClosedTestsNumberPerTest = await _service.GetClosedTests(testsListModel.PageNumber, testsListModel.PageSize, testsListModel.TextFilter);
                var testsId = (from t in testsListModel.Tests
                                        select t.Id).ToList();
                testsListModel.AdministratedTestOpen = await _service.OpenedTestNumber(testsId);
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
            try
            {
                await _service.DeleteTest(test.Id);
                Logger.Info("Successfully deleted test");
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
            TestDto testToPass = new TestDto(administratedservice);

            try
            {
                testToPass = new TestDto(await _service.GetTestById(test.Id), _service);
                testToPass.PageNumber = test.PageNumber;
                testToPass.PageSize = test.PageSize;
                testToPass.URL = _service.GenerateUrl(testToPass.URL);
                testToPass.TextFilter = test.TextFilter;
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
        public async Task<ActionResult> DeleteQuestion(QuestionDto question, int TestId)
        {
            var returned = new TestDto();
            try
            {
                await _service.DeleteQuestionByIdFromTest(question.Id);
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


            var test = await _service.GetTestById(TestId);
            returned = new TestDto(test, _service);
            returned.ShowForm = true;
            
            return View("Index", returned);
        }

        [HttpPost]
        public async Task<ActionResult> TextSearch(TestsListModel testsListModel)
        {
            if (String.IsNullOrWhiteSpace(testsListModel.TextFilter))
            {
                return RedirectToAction("TestsList", testsListModel);
            }

            var tests = await _service.GetTestsByFilter(testsListModel.TextFilter);

            testsListModel.Tests = tests.Select(t => new TestDto(t, _service)).ToList();
            

            testsListModel.PageNumber = 1;
            testsListModel.PageSize = 10;

            await testsListModel.Paginate(testsListModel.Tests);

            testsListModel.ClosedTestsNumberPerTest = await _service.GetClosedTests(testsListModel.PageNumber, testsListModel.PageSize, testsListModel.TextFilter);

            var testsId = (from t in testsListModel.Tests
                select t.Id).ToList();
            testsListModel.AdministratedTestOpen = await _service.OpenedTestNumber(testsId);

            return View("TestsList", testsListModel);
        }

        public async Task<JsonResult> SendMail(EmailModel emailModel)
        {
            TestBusiness test;

            try
            {
                test = await _service.GetTestById(emailModel.Id);
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
            TestDto sendTest = new TestDto(test, _service);
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

        public ActionResult GetAddQuestionPartial(TestDto model)
        {
            var myModel = new QuestionDto();
            myModel.TestId = model.Id;
            return PartialView("AddQuestionPartial", myModel);
        }

        [HttpGet]
        public async Task<ActionResult> QuestionDetails(QuestionDto question)
        {
            var questionDomain = await _service.GetQuestionById(question.Id);
            var questionDao = new QuestionDto(questionDomain);
            return PartialView("Index", questionDao);
        }
        [HttpPost]
        public async Task<ActionResult> EditQuestionsAsync(QuestionDto questionmodel)
        {
            try
            {
                var testBusiness = await _service.GetTestById(questionmodel.TestId);
                var testDTO = new TestDto(testBusiness, _service);
                var AnswerDTO = testDTO.Questions.Where(x => x.Id == questionmodel.Id).
                    SelectMany(x => x.Answers).ToList();
                questionmodel.Answers = AnswerDTO;
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
            return View("EditQuestionsAsync", questionmodel);
        }

        [HttpPost]
        public ActionResult SaveUpdateQuestion(QuestionDto editModel)
        {
            //TO IMPLEMENT
            return View("TestContent");
        }
    }
}
