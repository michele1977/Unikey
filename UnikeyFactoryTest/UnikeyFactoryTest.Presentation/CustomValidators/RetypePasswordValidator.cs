using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    //public class RetypedPassword : ValidationAttribute
    //{
    //    private string _propertyName;

    //    public RetypedPassword(string propertyName)
    //    {
    //        _propertyName = propertyName;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var retypedPassword = value as string;
    //        var property = validationContext.ObjectType.GetProperty(_propertyName);
    //        if (property != null)
    //        {
    //            //e' password che diventa null
    //            var password = property.GetValue(validationContext.ObjectInstance, null).ToString();
    //            if (retypedPassword != password)
    //            {
    //                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        else
    //        {
    //            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
    //        }

    //    }
    //}


    public class RetypedPassword : ValidationAttribute
    {
        private string _propertyName;

        public RetypedPassword(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var retypedPassword = value as string;
            var property = validationContext.ObjectType.GetProperty(_propertyName);
            try
            {
                var password = property.GetValue(validationContext.ObjectInstance, null).ToString();
                if (retypedPassword != password)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
        }

    }
    
}