using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class ExTestController : Controller
    {
        private static int UserId { get; set; }
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly AdministratedTestService _adTestService = new AdministratedTestService();
        private readonly TestService _testService = new TestService();
        // GET: AdministratedTest

        public ActionResult TestStart(string guid)
        {
            var model = new AdministratedTestModel();

            model.Url = guid;


            return View("TestStart", model);
        }
        // test
        [HttpPost]
        public async Task<ActionResult> BeginTest(AdministratedTestModel model)
        {
            var subject = model.Name + " " + model.Surname;
            var test = _testService.GetTestByURL(model.Url);
            var newExecutionTest = _adTestService.AdministratedTest_Builder(test, subject);
            var savedTest = await _adTestService.Add(newExecutionTest);
            model.NumQuestion = test.Questions.Count;
            model.ActualQuestion = savedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == 0);
            model.AdministratedTestId = savedTest.Id;
            //await service.ChangeAdministratedTestStateToStarted(savedTest.Id);
            return View("Test", model);
        }

        public async Task<ActionResult> SaveTest(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await _adTestService.GetAdministratedTestById(model.AdministratedTestId);
            var actualQuestion = administratedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == model.ActualPosition);

            if (Request.Form[actualQuestion.Id.ToString()] != null)
            {
                var value = Request.Form[actualQuestion.Id.ToString()];
                foreach (var administratedAnswer in actualQuestion.AdministratedAnswers)
                {
                    if (administratedAnswer.isSelected)
                    {
                        administratedAnswer.isSelected = false;
                    }
                }
                actualQuestion.AdministratedAnswers.FirstOrDefault(a => a.Id == System.Convert.ToInt32(value)).isSelected = true;
                //await service.Update_Save_Question(actualQuestion);
            }


            administratedTest.State = AdministratedTestState.Closed;
            await _adTestService.Update_Save(administratedTest);
            return View("TestEnded");
        }


        //[HttpGet]
        //public async Task<ActionResult> AdministratedTestsList(AdministratedTestsListModel testsListModel)
        //{
        //    testsListModel = testsListModel ?? new AdministratedTestsListModel();

        //    AdministratedTestService service = new AdministratedTestService();

        //    var tests = await service.GetAdministratedTests();

        //    testsListModel.Tests = testsListModel.Paginate(tests.ToList());

        //    return View(testsListModel);
        //}

        [HttpGet]
        public async Task<ActionResult> AdministratedTestsList(AdministratedTestsListModel adTestsListModel)
        {
            adTestsListModel = adTestsListModel ?? new AdministratedTestsListModel();

            try
            {
                var adTests = (adTestsListModel.TextFilter.IsNullOrWhiteSpace())
                    ? (await _adTestService.GetAdministratedTests()).ToList() : (await _adTestService.GetAdministratedTestsByFilter(adTestsListModel.TextFilter)).ToList();

                adTestsListModel.Tests = adTestsListModel.Paginate(adTests);
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

            return View(adTestsListModel);
        }

        [HttpGet]
        public async Task<ActionResult> AdministratedTestContent(AdministratedTestDto test)
        {
            AdministratedTestService service = new AdministratedTestService();
            var testToPass = new AdministratedTestDto(await service.GetAdministratedTestById(test.Id));
            testToPass.PageNumber = test.PageNumber;
            testToPass.PageSize = test.PageSize;
            return View(testToPass);
        }

        [HttpPost]
        public async Task<ActionResult> Next(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await _adTestService.GetAdministratedTestById(model.AdministratedTestId);
            var actualQuestion = administratedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == model.ActualPosition);
            if (Request.Form[actualQuestion.Id.ToString()] != null)
            {
                var value = Request.Form[actualQuestion.Id.ToString()];
                foreach (var administratedAnswer in actualQuestion.AdministratedAnswers)
                {
                    if (administratedAnswer.isSelected)
                    {
                        administratedAnswer.isSelected = false;
                    }
                }
                actualQuestion.AdministratedAnswers.FirstOrDefault(a => a.Id == System.Convert.ToInt32(value)).isSelected = true;
                await _adTestService.Update_Save_Question(actualQuestion);
            }
            model.ActualQuestion = await _adTestService.Next(administratedTest, model.ActualPosition + 1);
            return View("Test", model);
        }

        [HttpPost]
        public async Task<ActionResult> Close(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await _adTestService.GetAdministratedTestById(model.AdministratedTestId);
            var actualQuestion = administratedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == model.ActualPosition);

            if (Request.Form[actualQuestion.Id.ToString()] != null)
            {
                var value = Request.Form[actualQuestion.Id.ToString()];
                foreach (var administratedAnswer in actualQuestion.AdministratedAnswers)
                {
                    if (administratedAnswer.isSelected)
                    {
                        administratedAnswer.isSelected = false;
                    }
                }
                actualQuestion.AdministratedAnswers.FirstOrDefault(a => a.Id == System.Convert.ToInt32(value)).isSelected = true;
                //await service.Update_Save_Question(actualQuestion);
            }

            administratedTest.State = AdministratedTestState.Open;
            await _adTestService.Update_Save(administratedTest);
            return View("TestEnded");
        }
        public async Task<ActionResult> Previous(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await _adTestService.GetAdministratedTestById(model.AdministratedTestId);
            var actualQuestion = administratedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == model.ActualPosition);
            if (Request.Form[actualQuestion.Id.ToString()] != null)
            {
                var value = Request.Form[actualQuestion.Id.ToString()];
                foreach (var administratedAnswer in actualQuestion.AdministratedAnswers)
                {
                    if (administratedAnswer.isSelected)
                    {
                        administratedAnswer.isSelected = false;
                    }
                }
                actualQuestion.AdministratedAnswers.FirstOrDefault(a => a.Id == System.Convert.ToInt32(value)).isSelected = true;
                await _adTestService.Update_Save_Question(actualQuestion);
            }
            model.ActualQuestion = await _adTestService.Previous(administratedTest, model.ActualPosition - 1);
            return View("Test", model);
        }
        [HttpGet]
        public async Task<ActionResult> DetailsTablePartial(int testId)
        {
            AdministratedTestService service = new AdministratedTestService();
            AdministratedTestDto testToPass = new AdministratedTestDto();
            try
            {
                var theTest = await service.GetAdministratedTestsByTestId(testId);
                testToPass.AdministratedTests = theTest;
            }
            catch(Exception e)
            {
                
            }
            return PartialView("DetailsTablePartial", testToPass/*,*//*TestVisual*/);
        }

        [HttpPost]
        public async Task<ActionResult> TextSearch(AdministratedTestsListModel adTestsListModel)
        {
            if (String.IsNullOrWhiteSpace(adTestsListModel.TextFilter))
            {
                return RedirectToAction("AdministratedTestsList", adTestsListModel);
            }

            var tests = await _adTestService.GetAdministratedTestsByFilter(adTestsListModel.TextFilter);

            adTestsListModel.Tests = tests.Select(t => new AdministratedTestDto(t)).ToList();


            adTestsListModel.PageNumber = 1;
            adTestsListModel.PageSize = 10;

            adTestsListModel.Paginate(adTestsListModel.Tests);

            return View("AdministratedTestsList", adTestsListModel);
        }
    }
}