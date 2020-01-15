using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Presentation.CustomValidators;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter a username")]
        //[MaxLength(10,ErrorMessage = "MaxLength Error")]
        //[MinLength(4, ErrorMessage = "MinLength Error")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please retype the password")]
        [RetypedPassword("Password")]
        public string RetypedPassword { get; set; }

        public string IsUser { get; set; }

        public UserModel()
        {

        }
    }
}