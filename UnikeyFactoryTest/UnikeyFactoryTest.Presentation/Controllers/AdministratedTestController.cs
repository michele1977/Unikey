using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class AdministratedTestController : Controller
    {
        private AdministratedTestService service = new AdministratedTestService();
        private TestService testService = new TestService();
        // GET: AdministratedTest
        
        public ActionResult TestStart()
        {
            var model = new AdministratedTestModel();
            return View("TestStart", model);
        }

        [HttpPost]
        public ActionResult BeginTest(AdministratedTestModel model)
        {
            var subject = model.Name + " " + model.Surname;
            //creo un test temporaneo da sostire con quello repertio dalla URL
            //var test = testService.GetTestById(model.URL);
            var test = new TestBusiness();
            model.Test = AdministratedTestMapper.MapDomainToDao(service.AdministratedTest_Builder(test, subject));
            return View("Test", model);
        }

        public ActionResult SaveTest(AdministratedTestModel model, FormCollection form)
        {
            //popolo il dictionary con domanda e relativa risposta
            foreach (var key in form.AllKeys)
            {
                model.QuestionAnswerDictionary[int.Parse(key)] = int.Parse(form[key]);
            }

            //var test = testService.GetTestById(model.URL);
            //var testQuestions = service.AdministratedTest_Builder(model.URL).Questions;
            

            foreach (var question in model.QuestionAnswerDictionary)
            {
                //var specificQuestion = testQuestions.SingleOrDefault(x => x.Id == question.Key);
                //if (specificQuestion != null)
                //{
                //    var theAnswer = specificQuestion.Answers.SingleOrDefault(x => x.Id == question.Value);
                //    theAnswer.
                //}
            }
            
            return View("TestStart");
        }
    }
}