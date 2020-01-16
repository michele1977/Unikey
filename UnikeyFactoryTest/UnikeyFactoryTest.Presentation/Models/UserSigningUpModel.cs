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
        //Custom Validator con Regex
        public string Password { get; set; }

        [RetypedPassword("Password", ErrorMessage = "The passwords don't match")]
        [Required(ErrorMessage = "Please retype the password")]
        public string RetypedPassword { get; set; }

        public UserSigningUpModel()
        {
            
        }

    }
}