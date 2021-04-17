using System.Collections.Generic;
using DataLayer.BaseModels;

namespace JobsWebApp.ViewModels.Admin
{
    public class AdminControllerIndexViewModel
    {
        public List<VacancyBaseModel> PublishedVacancies { get; set; }
        public List<VacancyBaseModel> DraftVacancies { get; set; }
    }
}