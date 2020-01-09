using BLL;
using Presentazione.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Presentazione.Controllers.UserController;

namespace Presentazione.Models
{
    public class UserRegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ErrorState Check = ErrorState.Empty;
    }
}