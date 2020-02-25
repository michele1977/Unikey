using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using UnikeyFactoryTest.Presentation.Models.DTO;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    public class QuestionValidator : AbstractValidator<QuestionDto>
    {
        public QuestionValidator()
        {

            RuleFor(question => question.Text).NotEmpty().WithMessage("Question's {PropertyName} can't be empty");
            RuleForEach(question => question.Answers).ChildRules(a =>
            {
                a.RuleFor(answer => answer.Text).NotEmpty().WithMessage("Answer's {PropertyName} can't be empty");
                //a.RuleFor(answer => answer.Score)
                //    .NotEmpty()
                //    .When(answer => answer.IsCorrectBool, ApplyConditionTo.CurrentValidator)
                //    .WithMessage("{PropertyName} field is required for correct answer")
                //    .Must(s => s >= 0)
                //    .WithMessage("{PropertyName} must be a positive number");

            });


        }
    }
}