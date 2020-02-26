using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Presentation.Models;
using System.Web;

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
            model.AreThereMessages = false;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(UserLoginModel model)
        {
            var userViewModel = new UserModel();
            if (ModelState.IsValid)
            {
                var signInManager = HttpContext.GetOwinContext().Get<SignInManager<UserBusiness, int>>();
                var signInStatus =
                    await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (signInStatus == SignInStatus.Success)
                {
                    return RedirectToAction("TestsList", "Test");
                }

                ModelState.Clear();
                userViewModel.AreThereMessages = true;
                userViewModel.ToForm = ToForm.LoginForm;
                userViewModel.LoginModel.UserState = UserState.IsNotAUser;
                return View("Index", userViewModel);
            }

            ModelState.Clear();
            userViewModel.AreThereMessages = false;
            userViewModel.LoginModel.UserState = UserState.WaitingFor;
            return View("Index", userViewModel);
        }

        public async Task<ActionResult> Subscribe(UserSigningUpModel model)
        {
            var userViewModel = new UserModel();

            if (ModelState.IsValid)
            {
                var user = new UserBusiness() { UserName = model.Username, Password = model.Password };

                var result = await service.CreateAsync(user);

                if(result.Errors.Count() != 0)
                {
                    userViewModel.AreThereMessages = false;
                    userViewModel.ToForm = ToForm.SigningUpForm;
                    userViewModel.SigningUpModel.UserState = UserState.RegistrationKo2;
                }
                return View("Index", userViewModel);
            }

            userViewModel.SigningUpModel.UserState = UserState.RegistrationKo1;
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
            model.LoginModel.UserState = UserState.WaitingFor;
            model.AreThereMessages = false;
            return View("Index", model);
        }
        
        public ActionResult GetLoginPartial(UserLoginModel model)
        {
            ModelState.Clear();
            return PartialView("_LoginFormPartial", model);
        }

        public ActionResult GetSignUpPartial(UserSigningUpModel model)
        {
            var newModel = new UserSigningUpModel();
            newModel.UserState = model.UserState;
            ModelState.Clear();
            return PartialView("_SigningUpFormPartial", newModel);
        }
    }
}