using System.Collections.Generic;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.ViewModels
{
    public class CreateViewModel
    {
        public int Id { get; set; }


        public VacancyApplicationViewModel VacancyApplication { get; set; }

        public List<VacancyEducationViewModel> Education { get; set; }

        public List<SelectListItem> EducationTypes { get; set; }

        public List<VacancyWorkHistoryViewModel> WorkHistory { get; set; }

        public List<VacancyCustomQuestionModel> Questions { get; set; }

        public List<VacancyCustomQuestionAnswerViewModel> Answers { get; set; }
    }

}