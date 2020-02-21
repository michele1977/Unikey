using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
using Microsoft.Ajax.Utilities;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers                    
{
    public class ExTestController : Controller
    {
        private IAdministratedTestService _adTestService;
        private ITestService _testService;
        private Logger Logger = LogManager.GetCurrentClassLogger();
        private IKernel kernel;
        public ExTestController(IAdministratedTestService value, ITestService value2, IKernel kernel)
        {
            _testService = value2;
            this.kernel = kernel;
            _adTestService = value;
        }

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
            var test = await _testService.GetTestByURL(model.Url);
            var newExecutionTest = _adTestService.AdministratedTest_Builder(test, subject);
            newExecutionTest.State = AdministratedTestState.Open;
            var savedTest = _adTestService.Add(newExecutionTest);
            model.NumQuestion = test.Questions.Count;
            model.ActualQuestion = savedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == 0);
            model.AdministratedTestId = savedTest.Id;
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
                await _adTestService.Update_Save_Question(actualQuestion);
            }


            administratedTest.State = AdministratedTestState.Closed;
            await _adTestService.Update_Save(administratedTest);
            return View("TestEnded");
        }

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
            var testToPass = new AdministratedTestDto(await _adTestService.GetAdministratedTestById(test.Id));
            testToPass.PageNumber = test.PageNumber;
            testToPass.PageSize = test.PageSize;
            testToPass.TextFilter = test.TextFilter;
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
            model.ActualQuestion = _adTestService.Next(administratedTest, model.ActualPosition + 1);
            return View("Test", model);
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
            model.ActualQuestion = _adTestService.Previous(administratedTest, model.ActualPosition - 1);
            return View("Test", model);
        }
        [HttpGet]
        public async Task<ActionResult> DetailsTablePartial(int testId)
        {
            AdministratedTestDto testToPass = new AdministratedTestDto();
            try
            {
                var theTest = await _adTestService.GetAdministratedTestsByTestId(testId);
                testToPass.AdministratedTests = theTest;
            }
            catch(Exception e)
            {
                
            }
            return PartialView("DetailsTablePartial", testToPass);
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