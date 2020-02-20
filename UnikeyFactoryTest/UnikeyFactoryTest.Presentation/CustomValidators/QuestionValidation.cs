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
       
            RuleFor(x => x.Text).NotEmpty().WithMessage("You must write the question");
            RuleForEach(x => x.Answers).Where(d=> d.IsCorrectBool == true).ChildRules(a =>
            {
                a.RuleFor(x => x.Score).NotEmpty().WithMessage("You must write answers");
                RuleFor(x => x.Answers).Must(c => c.Count > 0).WithMessage("chek  a correct question");

            });
            RuleForEach(x => x.Answers).ChildRules(a =>
            {
                a.RuleFor(x => x.Text).NotEmpty().WithMessage("You must write answers");
            });
         

        }

        //private bool CheckScore()
        //{
        //    QuestionDto question = new QuestionDto();
        //     var res = (question.Answers.Where(x => x.IsCorrect == AnswerState.Correct)).Count();

        //    if (res > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }

    

}