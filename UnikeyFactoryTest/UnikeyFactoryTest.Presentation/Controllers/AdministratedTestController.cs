using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class AdministratedTestController : Controller
    {
        private AdministratedTestService service;
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
            var test = new Test();
            model.Test = service.AdministratedTest_Builder(test, subject);
            return View("Test", model);
        }
    }
}