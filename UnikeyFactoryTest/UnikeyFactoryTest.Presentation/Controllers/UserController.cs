using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Ajax.Utilities;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Presentation.Models;

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
            //model.AreThereMessages = false;
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
        [HttpPost]
        public async Task<ActionResult> LogIn(UserLoginModel model)
        {
            var userViewModel = new UserModel();
            if (ModelState.IsValid)
            {
                //var user = new User { Username = model.Username, Password = model.Password };

//var result = await service.IsUser(user);

                //if (result == true)
                var signInManager = HttpContext.GetOwinContext().Get<SignInManager<UserBusiness, int>>();
                var signInStatus = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (signInStatus == SignInStatus.Success)
                {
                    //user.Id = await service.GetUserIdByUsername(user);
                    //HttpContext.Session["UserId"] = user.Id;

                    return RedirectToAction("TestsList", "Test");
                }

                userViewModel.UserState = UserState.IsNotAUser;
                return View("Index", userViewModel);

                //else
                //{
                //    ModelState.Clear();
                //    newModel.AreThereMessages = true;
                //    newModel.ToForm = ToForm.LoginForm;
                //    newModel.LoginModel.UserState = UserState.IsNotAUser;
                //    return View("Index", newModel);
                //}
            }
            //else
            //{
            //    ModelState.Clear();
            //    newModel.AreThereMessages = false;
            //    newModel.LoginModel.UserState = UserState.WaitingFor;
            //    return View("Index", newModel);
            //}


            userViewModel.UserState = UserState.WaitingFor;
            return View("Index", userViewModel);

        }

        public async Task<ActionResult> Subscribe(UserSigningUpModel model)
        {
            var userViewModel = new UserModel();

            if (ModelState.IsValid)
            {
                //User user = new User() { Username = model.Username, Password = model.Password };

                //service.InsertUser(user);

                //newModel.AreThereMessages = true;
                //newModel.ToForm = ToForm.SigningUpForm;
                //newModel.SigningUpModel.UserState = UserState.RegistrationOk;
                //return View("Index", newModel);
            }
            else
            {
                //newModel.AreThereMessages = true;
                //newModel.ToForm = ToForm.SigningUpForm;
                //newModel.SigningUpModel.UserState = UserState.RegistrationKo;
                //return View("Index", newModel);
                var user = new UserBusiness() {UserName = model.Username, Password = model.Password};
                var result = await service.CreateAsync(user);

                if(result.Errors.Count() != 0)
                {
                    userViewModel.UserState = UserState.RegistrationKo2;
                    return View("Index", userViewModel);
                }

                userViewModel.UserState = UserState.RegistrationOk;
                return View("Index", userViewModel);
            }

            userViewModel.UserState = UserState.RegistrationKo1;
            return View("Index", userViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            var model = new UserModel();
            if (Request.Cookies[".AspNet.ApplicationCookie"] != null)
            {
                HttpCookie myCookie = new HttpCookie(".AspNet.ApplicationCookie");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            model.UserState = UserState.WaitingFor;
            //model.AreThereMessages = false;
            return View("Index", model);
        }



        //public ActionResult GetLoginPartial(UserState userState)
        //{
        //    var newModel = new UserLoginModel();
        //    newModel.UserState = userState;
        //    return PartialView("_LoginFormPartial", newModel);
        //}

        //public ActionResult GetLoginPartial(UserLoginModel model)
        //{
        //    var newModel = new UserLoginModel();
        //    newModel.UserState = model.UserState;
        //    ModelState.Clear();
        //    return PartialView("_LoginFormPartial", newModel);
        //}

        //public ActionResult GetSignUpPartial(UserSigningUpModel model)
        //{
        //    var newModel = new UserSigningUpModel();
        //    newModel.UserState = model.UserState;
        //    ModelState.Clear();
        //    return PartialView("_SigningUpFormPartial", newModel);
        //}
    }
}