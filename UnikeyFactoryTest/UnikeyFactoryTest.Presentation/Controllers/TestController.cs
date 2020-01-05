using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Presentation.Models.Dto;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult TestsList(TestsListModel testsListModel)
        {
            testsListModel = testsListModel ?? new TestsListModel();

            TestService service = new TestService();

            testsListModel.Tests = testsListModel.Paginate(service.GetTests().ToList());

            if (testsListModel.IsAjaxCall)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("TestsList",
                        new { PageNumber = testsListModel.PageNumber, PageSize = testsListModel.PageSize })
                });
            }

            return View(testsListModel);
        }

        [HttpPost]
        public JsonResult DeleteTest(TestDto test)
        {
            TestService service = new TestService();
            service.DeleteTest(test.Id);

            return Json(new
            {
                redirectUrl = Url.Action("TestsList",
                    new { PageNumber = test.PageNumber, PageSize = test.PageSize })
            });
        }

        [HttpGet]
        public ActionResult TestContent(TestDto test)
        {
            TestService service = new TestService();
            TestDto testToPass = new TestDto(service.GetTestById(test.Id));
            return View(testToPass);
        }

        [HttpPost]
        public ActionResult TextSearch(TestsListModel testsListModel)
        {
            if (!testsListModel.TextFilter.IsNullOrWhiteSpace())
            {
                TestService service = new TestService();

                testsListModel.Tests = service.GetTests()
                    .Where(t => t.User.Username.Contains(testsListModel.TextFilter))
                    .Select(t => new TestDto(t)).ToList();
            }

            return RedirectToAction("TestsList");
        }
    }
}