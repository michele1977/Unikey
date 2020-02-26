using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    public class StrongPassword : ValidationAttribute
    {
        public StrongPassword() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var propertyValue = value as string;
                Regex rex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[*@$!#%&()^_~{}+=|.]).{6,50}$");

                if (rex.IsMatch(propertyValue) == true)
                {
                    return null;
                }
                else
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
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