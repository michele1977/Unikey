using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.Identity.Owin;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Presentation;
using UnikeyFactoryTest.Presentation.Models;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class UserController : Controller
    {
        private UserManager<UserBusiness, int> service;
         
        public UserController(UserManager<UserBusiness, int> value)
        {
            service = value;
        }
        public ActionResult Index()
        {
            var model = new UserModel();
            model.UserState = UserState.WaitingFor;
            
            return View(model);
        }


        //[HttpPost]
        //public async Task<ActionResult> CheckData(UserLoginModel model)
        //{
        //    var userViewModel = new UserModel();

        //    if (ModelState.IsValid)
        //    {
        //        var user = new User {Username = model.Username, Password = model.Password};

        //        var result = await service.IsUser(user);

        //        if (result == true)
        //        {
        //            user.Id = await service.GetUserIdByUsername(user);
        //            HttpContext.Session["UserId"] = user.Id;

        //            return RedirectToAction("TestsList", "Test");
        //        }
        //        else
        //        {
        //            userViewModel.UserState = UserState.IsNotAUser;
        //            return View("Index", userViewModel);
        //        }
        //    }
        //    else
        //    {
        //        userViewModel.UserState = UserState.WaitingFor;
        //        return View("Index", userViewModel);
        //    }
        //}

        public async Task<ActionResult> LogIn(UserLoginModel model)
        {
            var userViewModel = new UserModel();
            if (ModelState.IsValid)
            {
                var signInManager = HttpContext.GetOwinContext().Get<SignInManager<UserBusiness, int>>();
                var signInStatus = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (signInStatus == SignInStatus.Success)
                {
                    return RedirectToAction("TestsList", "Test");
                }

                userViewModel.UserState = UserState.IsNotAUser;
                return View("Index", userViewModel);

            }

            userViewModel.UserState = UserState.WaitingFor;
            return View("Index", userViewModel);

        }

        public async Task<ActionResult> Subscribe(UserSigningUpModel model)
        {
            var userViewModel = new UserModel();

            if (ModelState.IsValid)
            {
                var user = new UserBusiness() {UserName = model.Username, Password = model.Password};
                var result = await service.CreateAsync(user);

                if(result.Errors.Count() != 0)
                {
                    userViewModel.UserState = UserState.RegistrationKo;
                    return View("Index", userViewModel);
                }

                userViewModel.UserState = UserState.RegistrationOk;
                return View("Index", userViewModel);
            }

            userViewModel.UserState = UserState.RegistrationKo;
            return View("Index", userViewModel);
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