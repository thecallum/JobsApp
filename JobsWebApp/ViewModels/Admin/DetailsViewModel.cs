using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsWebApp.ViewModels.Admin
{
    public class DetailsViewModel
    {
        public VacancyModel Vacancy { get; set; }

        public DepartmentModel Department { get; set; }
    }
}
