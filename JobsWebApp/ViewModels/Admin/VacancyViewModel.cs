using System;
using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels.Admin
{
    public class VacancyViewModel
    {
        public int Id { get; set; }

        [Required] public string JobTitle { get; set; }

        [Required] public string JobDescription { get; set; }

        [Required] public int SalaryMin { get; set; }

        [Required] public int SalaryMax { get; set; }

        [Required] public string ContractType { get; set; }

        [Required] public DateTime? StartDate { get; set; }

        [Required] public DateTime? EndDate { get; set; }

        [Required] public bool Published { get; set; }
    }
}