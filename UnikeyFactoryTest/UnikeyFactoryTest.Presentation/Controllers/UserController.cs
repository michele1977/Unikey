using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult CheckData()
        {

            return View("Index");
        }

    }
}