using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Presentation.CustomValidators;

namespace UnikeyFactoryTest.Presentation.Models
{
    public enum UserState
    {
        WaitingFor,
        IsNotAUser,
        RegistrationOk,
        RegistrationKo1,
        RegistrationKo2
    }

    public class UserModel
    {
        public UserLoginModel LoginModel { get; set; }
        public UserSigningUpModel SigningUpModel { get; set; }

        public UserState UserState { get; set; }

        public UserModel()
        {
            LoginModel = new UserLoginModel();
            SigningUpModel = new UserSigningUpModel();
        }
    }

}