using System;
using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.CustomValidation
{
    public class IntFieldGreaterThanAnotherIntField : ValidationAttribute
    {
        [Required]
        public string IntToCompareFieldName { get; set; }    
        
        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field must be less than the {IntToCompareFieldName} field.";
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var intValue = value as int? ?? new int();

            var lesserValue = (int?)validationContext.ObjectType.GetProperty(IntToCompareFieldName).GetValue(validationContext.ObjectInstance, null);
            
            if (lesserValue >= intValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            }

            return ValidationResult.Success;
        }
    }
}