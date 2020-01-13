using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var model = new UserModel {IsUser = "WaitingForLogin"};
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CheckData(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User {Username = model.Username, Password = model.Password};
                var service = new UserService();

                var result = await service.IsUser(user);

                if (result == true)
                {
                    user.Id = await service.GetUserIdByUsername(user);
                    return RedirectToAction("Index", "Test", new {UserId = user.Id});
                }
                else
                {
                    model.IsUser = "IsNotAUser";
                    return View("Index", model);
                }
            }
            else
            {
                model.IsUser = "";
                //ModelState.AddModelError("", "Unable to rr");
                return View("Index", model);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var model = new UserModel();
            model.IsUser = "WaitingForLogin";
            return View("Index", model);
        }
    }


}