using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter User name.")]
        [MaxLength(10,ErrorMessage = "MaxLength Error")]
        [MinLength(4, ErrorMessage = "MinLength Error")]
        public string Username { get; set; }
        
        public string Password { get; set; }
        public string IsUser { get; set; }

        public UserModel()
        {

        }
    }
}