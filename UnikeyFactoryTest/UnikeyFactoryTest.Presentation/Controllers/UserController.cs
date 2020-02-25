using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Presentation.Models;

namespace UnikeyFactoryTest.Presentation.Controllers
{
    public class UserController : Controller
    {
        private IUserService service;

        public UserController(IUserService value)
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
        public async Task<ActionResult> CheckData(UserLoginModel model)
        {

            var newModel = new UserModel();

            if (ModelState.IsValid == true)
            {
                var user = new User { Username = model.Username, Password = model.Password };

                var result = await service.IsUser(user);

                if (result == true)
                {
                    user.Id = await service.GetUserIdByUsername(user);
                    HttpContext.Session["UserId"] = user.Id;

                    return RedirectToAction("TestsList", "Test");
                }
                else
                {
                    ModelState.Clear();
                    newModel.AreThereMessages = true;
                    newModel.ToForm = ToForm.LoginForm;
                    newModel.LoginModel.UserState = UserState.IsNotAUser;
                    return View("Index", newModel);
                }
            }
            else
            {
                ModelState.Clear();
                newModel.AreThereMessages = false;
                newModel.LoginModel.UserState = UserState.WaitingFor;
                return View("Index", newModel);
            }

        }


        public ActionResult Subscribe(UserSigningUpModel model)
        {
            var newModel = new UserModel();

            if (ModelState.IsValid == true)
            {
                User user = new User() { Username = model.Username, Password = model.Password };

                service.InsertUser(user);

                newModel.AreThereMessages = true;
                newModel.ToForm = ToForm.SigningUpForm;
                newModel.SigningUpModel.UserState = UserState.RegistrationOk;
                return View("Index", newModel);
            }
            else
            {
                newModel.AreThereMessages = true;
                newModel.ToForm = ToForm.SigningUpForm;
                newModel.SigningUpModel.UserState = UserState.RegistrationKo;
                return View("Index", newModel);
            }

        }


        [HttpGet]
        public ActionResult Logout()
        {
            var model = new UserModel();
            model.AreThereMessages = false;
            return View("Index", model);
        }



        //public ActionResult GetLoginPartial(UserState userState)
        //{
        //    var newModel = new UserLoginModel();
        //    newModel.UserState = userState;
        //    return PartialView("_LoginFormPartial", newModel);
        //}

        public ActionResult GetLoginPartial(UserLoginModel model)
        {
            var newModel = new UserLoginModel();
            newModel.UserState = model.UserState;
            ModelState.Clear();
            return PartialView("_LoginFormPartial", newModel);
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