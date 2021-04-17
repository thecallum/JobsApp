using DataLayer.BaseModels;

namespace JobsWebApp.ViewModels.Admin
{
    public class DetailsViewModel
    {
        public VacancyBaseModel VacancyBase { get; set; }

        public DepartmentBaseModel DepartmentBase { get; set; }

        public int NumberOfApplications { get; set; }
    }
}