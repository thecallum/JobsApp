using System;
using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.CustomValidation
{
    public class DateInFuture : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field cannot be in the past.";
        }
        
        protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            if (dateValue < DateTime.Today)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}