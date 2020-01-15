using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class ExTestController : Controller
    {
        private AdministratedTestService service = new AdministratedTestService();
        private TestService testService = new TestService();
        // GET: AdministratedTest

        public ActionResult TestStart(string guid)
        {
            var model = new AdministratedTestModel();

            model.URL = guid;


            return View("TestStart", model);
        }
        // test
        [HttpPost]
        public async Task<ActionResult> BeginTest(AdministratedTestModel model)
        {
            var subject = model.Name + " " + model.Surname;
            var test = testService.GetTestByURL(model.URL);
            model.Test = service.AdministratedTest_Builder(test, subject);
            var savedTest = await service.Add(model.Test);
            model.admnistratedTestId = savedTest.Id;
            model.Test = savedTest;
            return View("Test", model);
        }

        public async Task<ActionResult> SaveTest(AdministratedTestModel model, FormCollection form)
        {
            var AdminstratedTest = await service.GetAdministratedTestById(model.admnistratedTestId);
            model.QuestionAnswerDictionary = new Dictionary<int, int>();
            //popolo il dictionary con domanda e relativa risposta
            foreach (var key in form.AllKeys)
            {
                if (key != "URL" && key != "admnistratedTestId")
                {
                    var value = Request.Form[key];
                    model.QuestionAnswerDictionary[System.Convert.ToInt32(key)] = System.Convert.ToInt32(value);
                }
                
            }
            foreach (var question in model.QuestionAnswerDictionary)
            {
               
                if (question.Value != 0)
                {
                    AdminstratedTest.AdministratedQuestions.FirstOrDefault(q => q.Id == question.Key)
                        .AdministratedAnswers.FirstOrDefault(a => a.Id == question.Value).isSelected = true;
                }

                AdminstratedTest.AdministratedQuestions.FirstOrDefault(q => q.Id == question.Key).Text =
                    AdminstratedTest.AdministratedQuestions.FirstOrDefault(q => q.Id == question.Key).Text + " ";
            }
            service.Update_Save(AdminstratedTest);
            return View("TestEnded");
        }


        [HttpGet]
        public async Task<ActionResult> AdministratedTestsList(AdministratedTestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new AdministratedTestsListModel();

            AdministratedTestService service = new AdministratedTestService();

            var tests = await service.GetAdministratedTests();

            testsListModel.Tests = testsListModel.Paginate(tests.ToList());
            
            return View(testsListModel);
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
    }
}