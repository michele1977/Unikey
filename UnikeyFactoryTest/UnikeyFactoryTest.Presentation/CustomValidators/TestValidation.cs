using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using FluentValidation;
using UnikeyFactoryTest.Presentation.Models.DTO;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    public class TestValidation : AbstractValidator<TestLigthDto>
    {
        public TestValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Test Title is required");

           
        }
       
    }
}