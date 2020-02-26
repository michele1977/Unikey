using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

    public enum ToForm
    {
        LoginForm,
        SigningUpForm,
    }

    public class UserModel
    {
        public UserLoginModel LoginModel { get; set; }
        public UserSigningUpModel SigningUpModel { get; set; }
        public bool AreThereMessages { get; set; }
        public ToForm ToForm { get; set; }

        public UserModel()
        {
            LoginModel = new UserLoginModel();
            SigningUpModel = new UserSigningUpModel();
        }

    }

}