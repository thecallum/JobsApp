using System;
using System.ComponentModel.DataAnnotations;
using JobsWebApp.CustomValidation;

namespace JobsWebApp.ViewModels.Apply
{
    public class VacancyWorkHistoryViewModel
    {
        [Required] 
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [MaxLength(50)] 
        public string EmployerName { get; set; }

        [Required] 
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [MaxLength(50)]
        public string JobTitle { get; set; }

        [Required] 
        [DateInPast]
        public DateTime? StartDate { get; set; }

        [Required] 
        [DateInPast]
        [DateGreaterThanAnotherDate(DateToCompareFieldName = "StartDate")]
        public DateTime? EndDate { get; set; }

        [Required] 
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [MaxLength(4000)] 
        public string Summary { get; set; }
    }
}