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
            var model = new UserModel();
            model.UserState = UserState.WaitingFor;
            
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CheckData(UserLoginModel model)
        {
            var userViewModel = new UserModel();

            if (ModelState.IsValid)
            {
                var user = new User {Username = model.Username, Password = model.Password};
                var service = new UserService();

                var result = await service.IsUser(user);

                if (result == true)
                {
                    user.Id = await service.GetUserIdByUsername(user);
                    return RedirectToAction("Index", "Test");
                }
                else
                {
                    userViewModel.UserState = UserState.IsNotAUser;
                    return View("Index", userViewModel);
                }
            }
            else
            {
                userViewModel.UserState = UserState.WaitingFor;
                return View("Index", userViewModel);
            }
        }


        public ActionResult Subscribe(UserSigningUpModel model)
        {
            var userViewModel = new UserModel();

            if (ModelState.IsValid == true)
            {
                //Chiama service, invoca il metodo per aggiungere uno user
                userViewModel.UserState = UserState.RegistrationOk;
                return View("Index", userViewModel);
            }
            else
            {
                userViewModel.UserState = UserState.RegistrationKo;
                return View("Index", userViewModel);
            }
            
        }


        [HttpGet]
        public ActionResult Logout()
        {
            var model = new UserModel();
            model.UserState = UserState.WaitingFor;
            return View("Index", model);
        }
    }


}