using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    public class RetypedPassword : ValidationAttribute
    {
        private string _propertyName;

        public RetypedPassword(string propertyNames)
        {
            _propertyName = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var retypedPassword = value as string;

            var property = validationContext.ObjectType.GetProperty(_propertyName);
            var password = property.GetValue(validationContext.ObjectInstance, null).ToString();
            if (retypedPassword != password)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}