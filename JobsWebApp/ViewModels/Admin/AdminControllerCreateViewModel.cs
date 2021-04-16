using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobsWebApp.ViewModels
{
    public class AdminControllerCreateViewModel
    {
        public VacancyViewModel Vacancy { get; set; }

        public List<SelectListItem> Departments { get; set; }

         public List<SelectListItem> SalaryRanges { get; set; }

        [Required]
        public int? SalaryRange { get; set; }

        [Required]
        public int? Department { get; set; }

        public List<CustomQuestionViewModel> Questions { get; set; } = new List<CustomQuestionViewModel>()
        {
            // default first item
            new CustomQuestionViewModel()
        };
    }

    public class CustomQuestionViewModel
    {
        public int DisplayOrder { get; set; } = 1;
        [Required]
        public string Question { get; set; }
        public bool IsRequired { get; set; } = true;
        public int? MinLength { get; set; } = 0;
        public int? MaxLength { get; set; } = 0;
    }


    public class VacancyViewModel
    {
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public int SalaryMin { get; set; }

        [Required]
        public int SalaryMax { get; set; }

        [Required]
        public string ContractType { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool Published { get; set; }


     

    }
}
