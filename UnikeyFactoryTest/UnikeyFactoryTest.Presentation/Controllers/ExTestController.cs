using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
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
        private IAdministratedTestService service;
        private ITestService testService;
        //private IKernel kernel;
        public ExTestController(IAdministratedTestService value, ITestService value2)
        {
            service = value;
            testService = value2;
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
            var test = testService.GetTestByURL(model.Url);
            var newExecutionTest = service.AdministratedTest_Builder(test, subject);
            newExecutionTest.State = AdministratedTestState.Started;
            var savedTest = await service.Add(newExecutionTest);
            model.NumQuestion = test.Questions.Count;
            model.ActualQuestion = savedTest.AdministratedQuestions.FirstOrDefault(x => x.Position == 0);
            model.AdministratedTestId = savedTest.Id;
            //await service.ChangeAdministratedTestStateToStarted(savedTest.Id);
            return View("Test", model);
        }

        public async Task<ActionResult> SaveTest(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await service.GetAdministratedTestById(model.AdministratedTestId);
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
            await service.Update_Save(administratedTest);
            return View("TestEnded");
        }


        [HttpGet]
        public async Task<ActionResult> AdministratedTestsList(AdministratedTestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new AdministratedTestsListModel();

            var tests = await service.GetAdministratedTests();

            testsListModel.Tests = testsListModel.Paginate(tests.ToList());

            return View(testsListModel);
        }

        [HttpGet]
        public async Task<ActionResult> AdministratedTestContent(AdministratedTestDto test)
        {
            var testToPass = new AdministratedTestDto(await service.GetAdministratedTestById(test.Id));
            testToPass.PageNumber = test.PageNumber;
            testToPass.PageSize = test.PageSize;
            return View(testToPass);
        }

        [HttpPost]
        public async Task<ActionResult> Next(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await service.GetAdministratedTestById(model.AdministratedTestId);
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
                await service.Update_Save_Question(actualQuestion);
            }
            model.ActualQuestion = await service.Next(administratedTest, model.ActualPosition + 1);
            return View("Test", model);
        }

        [HttpPost]
        public async Task<ActionResult> Close(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await service.GetAdministratedTestById(model.AdministratedTestId);
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
            await service.Update_Save(administratedTest);
            return View("TestEnded");
        }
        public async Task<ActionResult> Previous(AdministratedTestModel model, FormCollection form)
        {
            var administratedTest = await service.GetAdministratedTestById(model.AdministratedTestId);
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
                await service.Update_Save_Question(actualQuestion);
            }
            model.ActualQuestion = await service.Previous(administratedTest, model.ActualPosition - 1);
            return View("Test", model);
        }
        [HttpGet]
        public async Task<ActionResult> DetailsTablePartial(int testId)
        {
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
    }
}