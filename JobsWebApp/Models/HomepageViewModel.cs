using System.Collections.Generic;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.Models
{
    public class HomepageViewModel
    {
        public List<SelectListItem> Departments { get; set; }

        public List<SelectListItem> SalaryRanges { get; set; }

        public int? SalaryRange { get; set; }
        public int? Department { get; set; }

        public List<VacancyModel> Vacancies { get; set; }
    }
}