using BLL;
using Domain;
using Presentazione.Models;
using Repository;
using System.Web.Mvc;

namespace Presentazione.Controllers
{
    public class UserController : Controller
    {
        readonly UserService userService = new UserService(new ADO_UserRepository());

        [HttpGet]
        public ActionResult Registration()
        {
            UserRegistrationModel registrationModel = new UserRegistrationModel();
            ModelState.Clear();
            return View("Registration", registrationModel);
        }

        [HttpPost]
        public ActionResult Save(UserRegistrationModel registrationModel)
        {
            User utente = new User(registrationModel.Username, registrationModel.Password, registrationModel.Email);
            registrationModel = new UserRegistrationModel
            {
                Check = userService.Save(utente)
            };

            ModelState.Clear();
            return RedirectToAction("UsersList", registrationModel);
        }

        [HttpGet]
        public ActionResult UsersList()
        {
            UsersListModel usersListModel = new UsersListModel();
            usersListModel.UsersList = userService.GetUsersList();

            return View("UsersList", usersListModel);
        }

        [HttpPost]
        public ActionResult Delete(UsersListModel listModel)
        {
            userService.Delete(listModel.ID);

            ModelState.Clear();
            return RedirectToAction("UsersList");
        }
    }
}