using System;
using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels.Apply
{
    public class VacancyWorkHistoryViewModel
    {
        [Required] public string EmployerName { get; set; }

        [Required] public string JobTitle { get; set; }

        [Required] public DateTime? StartDate { get; set; }

        [Required] public DateTime? EndDate { get; set; }

        [Required] [MaxLength(300)] public string Summary { get; set; }
    }
}