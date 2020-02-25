using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Presentation.CustomValidators;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class UserSigningUpModel
    {
        [Required(ErrorMessage = "Please enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StrongPassword(ErrorMessage = "Invalid password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please retype the password")]
        [RetypedPassword("Password", ErrorMessage = "The passwords don't match")]
        public string RetypedPassword { get; set; }
        public UserState UserState { get; set; }

        public UserSigningUpModel()
        {
            
        }

    }
}