using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using UnikeyFactoryTest.Presentation.Models.DTO;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    public class TestValidator : AbstractValidator<TestDto>
    {
        public TestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Test {PropertyName} is required");
        }
    }
}