using System.Collections.Generic;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.ViewModels
{
    public class VacancyApplicationViewModel
    {
        public int Id { get; set; }


        public VacancyApplicationModel VacancyApplication { get; set; }

        public List<VacancyEducationViewModel> Education { get; set; }

        public List<SelectListItem> EducationTypes { get; set; }

        public List<VacancyWorkHistoryViewModel> WorkHistory { get; set; }

        public List<VacancyCustomQuestionModel> Questions { get; set; }

        public List<VacancyCustomQuestionAnswerViewModel> Answers { get; set; }
    }
}