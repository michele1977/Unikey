using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Presentation;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            UserModel model = new UserModel();
            model.IsUser = "WaitingForLogin";
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckData(UserModel model)
        {
            User user = new User();
            user.Username = model.Username;
            user.Password = model.Password;

            UserService service = new UserService();
            bool result = service.IsUser(user);

            if (result == true)
            {
                user.Id = service.GetUserIdByUsername(user);
                return RedirectToAction("Index", "Test", new {UserId = user.Id});
            }
            else
            {
                model.IsUser = "IsNotAUser";
                return View("Index", model);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            UserModel model = new UserModel();
            model.IsUser = "WaitingForLogin";
            return View("Index", model);
        }
    }


}