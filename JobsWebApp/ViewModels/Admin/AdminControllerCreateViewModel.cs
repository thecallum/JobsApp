using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.ViewModels.Admin
{
    public class AdminControllerCreateViewModel
    {
        public VacancyViewModel Vacancy { get; set; }

        public List<SelectListItem> Departments { get; set; }

        [Required] public int? Department { get; set; }

        public List<CustomQuestionViewModel> Questions { get; set; } = new()
        {
            // default first item
            new CustomQuestionViewModel()
        };
    }
}