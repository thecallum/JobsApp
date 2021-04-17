using System.Collections.Generic;
using DataLayer.BaseModels;
using DataLayer.FullModels;

namespace JobsWebApp.ViewModels.Admin
{
    public class ApplicationsViewModel
    {
        public VacancyBaseModel Vacancy { get; set; }

        public List<VacancyApplicationBaseModel> Applications { get; set; }

        public FullVacancyApplicationModel Applicant { get; set; }
    }
}