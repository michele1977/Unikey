using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class ExTestController : Controller
    {
        private AdministratedTestService service = new AdministratedTestService();
        private TestService testService = new TestService();
        // GET: AdministratedTest

        public ActionResult TestStart()
        {
            var model = new AdministratedTestModel();

            model.URL = Request.Url.AbsoluteUri;


            return View("TestStart", model);
        }

        [HttpPost]
        public ActionResult BeginTest(AdministratedTestModel model)
        {
            var subject = model.Name + " " + model.Surname;
            var test = testService.GetTestByURL(model.URL);
            model.Test = service.AdministratedTest_Builder(test, subject);
            model.Test.Test = testService.GetTestByURL(model.URL);
            var savedTest = service.Add(model.Test);
            model.admnistratedTestId = savedTest.Id;
            model.Test = savedTest;
            return View("Test", model);
        }

        public ActionResult SaveTest(AdministratedTestModel model, FormCollection form)
        {
            var AdminstratedTest = service.GetAdministratedTestById(model.admnistratedTestId);
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

            }
            service.Update_Save(AdminstratedTest);
            return View("TestEnded");
        }
    }
}