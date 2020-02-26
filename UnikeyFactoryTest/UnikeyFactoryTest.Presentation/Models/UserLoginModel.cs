using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Please enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        public UserState UserState { get; set; }

        public UserLoginModel()
        {
            
        }
    }
}