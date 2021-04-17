using System.Collections.Generic;
using DataLayer.BaseModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.ViewModels.Vacancy
{
    public class HomepageViewModel
    {
        public List<SelectListItem> Departments { get; set; }

        public List<SelectListItem> SalaryRanges { get; set; }

        public int? SalaryRange { get; set; }
        public int? Department { get; set; }

        public List<VacancyBaseModel> Vacancies { get; set; }
    }
}