using System;
using System.ComponentModel.DataAnnotations;
using JobsWebApp.CustomValidation;

namespace JobsWebApp.ViewModels.Admin
{
    public class VacancyViewModel
    {
        public int Id { get; set; }

        [Required] 
        [Display(Name = "Job Title")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string JobTitle { get; set; }

        [Required] 
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Required] 
        [Display(Name = "Minimum Salary")] 
        [Range(1000, 1000000)]
        public int? SalaryMin { get; set; }

        [Required] 
        [Display(Name = "Maximum Salary")]
        [Range(1000, 1000000)]
        [IntFieldGreaterThanAnotherIntField(IntToCompareFieldName = "SalaryMin")]
        public int? SalaryMax { get; set; }

        [Required] 
        [Display(Name = "Contract Type")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(50)]
        public string ContractType { get; set; }

        [Required] 
        [DateInFuture]
        [Display(Name = "Advert Start Date")]
        public DateTime? StartDate { get; set; }

        [Required] 
        [DateInFuture]
        [DateGreaterThanAnotherDate(DateToCompareFieldName = "StartDate")]
        [Display(Name = "Advert End Date")]
        public DateTime? EndDate { get; set; }

        [Required] 
        [Display(Name = "Published")]
        public bool Published { get; set; }
    }
}