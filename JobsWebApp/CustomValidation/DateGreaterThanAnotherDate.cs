using System;
using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.CustomValidation
{
    public class DateGreaterThanAnotherDate : ValidationAttribute
    {
        [Required]
        public string DateToCompareFieldName { get; set; }    
        
        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field must be greater than the {DateToCompareFieldName} field.";
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateValue = value as DateTime? ?? new DateTime();

            var firstDate = (DateTime?)validationContext.ObjectType.GetProperty(DateToCompareFieldName).GetValue(validationContext.ObjectInstance, null);
            
            if (firstDate >= dateValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            }

            return ValidationResult.Success;
        }
    }
}