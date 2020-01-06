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
                return View("Yes");
            else
            {
                model.IsUser = "IsNotAUser";
                return View("Index", model);
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            UserModel model = new UserModel();
            model.IsUser = "WaitingForLogin";
            return View("Index", model);
        }
    }


}