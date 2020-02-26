﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Ajax.Utilities;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Presentation.CustomValidators;
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
        private readonly IKernel Kernel;


        public TestController()
        {

        }

        public TestController(ITestService value, IAdministratedTestService value2, IKernel kernel)
        {
            Kernel = kernel;
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
                if (test.Questions.Count == 0)
                {
                    questionBiz.Position = 0;
                }
                else
                {
                    questionBiz.Position = Convert.ToInt16(test.Questions.Count);
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
        public async Task<ActionResult> AddTest(TestDto model)
        {
            var testValidator = Kernel.Get<IValidator<TestDto>>();

            var validationResult = await testValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure failer in validationResult.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
            }
            else
            {
                try
                {
                    test.Id = model.Id;
                    test.Title = model.Title;
                    test.UserId = UserId;
                    test.URL = _service.GenerateGuid();
                    test.Date = model.Date;
                    var mapper = Kernel.Get<IMapper>("Heavy");
                    var testDomain = mapper.Map<Test, TestBusiness>(test);
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
            }

            return View("index", model);
        }

        [HttpGet]
        public async Task<ActionResult> TestsList(TestsListModel testsListModel)
        {
            UserId = System.Convert.ToInt32(HttpContext.Session["UserId"]);
            testsListModel = testsListModel ?? new TestsListModel(_service);

            List<TestBusiness> tests = new List<TestBusiness>();

            try
            {
                tests = testsListModel.TextFilter.IsNullOrWhiteSpace() ? await _service.GetTests() :
                    await _service.GetTestsByFilter(testsListModel.TextFilter);                                                                                                                                                                         

                testsListModel.Tests = testsListModel.Paginate(tests);

                var testIds = testsListModel.Tests.Select(t => t.Id).ToList();

                testsListModel.ClosedTestsNumberPerTest = await _service.GetExTestCountByState(testIds, AdministratedTestState.Closed);

                testsListModel.AdministratedTestOpen = await _service.GetExTestCountByState(testIds, AdministratedTestState.Open);

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

            testsListModel.Tests = (await _service.GetTestsByFilter(testsListModel.TextFilter))
                .Select(t => new TestDto(t, _service)).ToList();

            testsListModel.Tests = await testsListModel.Paginate(testsListModel.Tests);

            var testIds = testsListModel.Tests.Select(t => t.Id).ToList();

            testsListModel.ClosedTestsNumberPerTest = await _service.GetExTestCountByState(testIds, AdministratedTestState.Closed);

            testsListModel.AdministratedTestOpen = await _service.GetExTestCountByState(testIds, AdministratedTestState.Open);

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
        public async Task<ActionResult> QuestionDetails(int questionId)
        {
            var questionDomain = await _service.GetQuestionById(questionId);
            var questionDao = new QuestionDto(questionDomain);

            if (questionDao.Answers.Count < 4)
            {
                int ansCount = questionDao.Answers.Count;
                for (int i = 0; i < 4 - ansCount; ++i)
                {
                    questionDao.Answers.Add(new AnswerDto());
                }
            }

            return PartialView("AddQuestionPartial", questionDao);
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
        public async Task<ActionResult> SaveUpdateQuestion(QuestionDto questionModel)
        {
            var questionBusiness = questionModel.MapToDomain();
            await _service.UpdateQuestion(questionBusiness);
            var testDTO = new TestDto(await _service.GetTestById(questionModel.TestId), _service);

            return View("TestContent", testDTO);
        }
        //[HttpPost]
        //public async Task<ActionResult> UpdateQuestion(QuestionDto question)
        //{
        //    var questionBusiness = question.MapToDomain();
        //    await _service.UpdateQuestion(questionBusiness);
        //    var testToModel = await _service.GetTestById(question.TestId);
        //    var model = new TestDto(testToModel, _service);
        //    model.ShowForm = true;
        //    return View("index",model);
        //}
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateQuestion(QuestionDto model)
        {
            var returned = new TestDto();

            QuestionValidator val = new QuestionValidator();
            ValidationResult res = val.Validate(model);

            if (!res.IsValid ||)
            {
                foreach (ValidationFailure err in res.Errors)
                {
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);

                }

                
                //for (int i = 0; i < model.Answers.Count; ++i)
                //{
                //    if (ModelState.ContainsKey($"Answers[{i}].Score"))
                //        ModelState[$"Answers[{i}].Score"].Errors.Clear();
                //}
                returned.Id = model.TestId;
                returned = new TestDto(await _service.GetTestById(returned.Id), _service);
                returned.ShowForm = true;
                returned.ShowPartial = true;
            }
            else
            {
                try
                {
                    var questionBiz = model.MapToDomain();

                    questionBiz.Answers.RemoveAll(a => { return a.IsCorrect == AnswerState.NotCorrect && (a.Text.IsNullOrWhiteSpace()); });

                    await _service.AddOrUpdateQuestion(questionBiz);
                    var test = await _service.GetTestById(questionBiz.TestId);

                    returned = new TestDto(test, _service);
                    returned.ShowForm = true;
                    returned.ShowPartial= false;
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
                
            }
            return View("Index", returned);
        }

    }
}
