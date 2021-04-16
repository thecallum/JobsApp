using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsWebApp.ViewModels
{
    public class AdminControllerIndexViewMovel
    {
        public List<VacancyModel> PublishedVacancies { get; set; }
        public List<VacancyModel> DraftVacancies { get; set; }
    }
}
