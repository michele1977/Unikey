using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.Presentation.Models.DTO;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    public class QuestionValidation : AbstractValidator<QuestionDto>
    {
        public QuestionValidation()
        {
       
            RuleFor(question => question.Text).NotEmpty().WithMessage("Question's text can't be empty");
            RuleForEach(question => question.Answers).ChildRules(a =>
            {
                a.RuleFor(answer => answer.Text).NotEmpty().WithMessage("Answer's text can't be empty");
                a.RuleFor(answer => answer.Score)
                    .NotEmpty()
                    .When(answer => answer.IsCorrectBool, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Score field is required for correct answer")
                    .Must(s => s >= 0)
                    .WithMessage("Score must be a positive number");

            });
            

        }

    }
    
}