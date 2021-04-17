using System.Collections.Generic;
using DataLayer.BaseModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobsWebApp.ViewModels.Apply
{
    public class CreateViewModel
    {
        public int Id { get; set; }


        public VacancyApplicationViewModel VacancyApplication { get; set; }

        public List<VacancyEducationViewModel> Education { get; set; }

        public List<SelectListItem> EducationTypes { get; set; }

        public List<VacancyWorkHistoryViewModel> WorkHistory { get; set; }

        public List<VacancyQuestionBaseModel> Questions { get; set; }

        public List<VacancyCustomQuestionAnswerViewModel> Answers { get; set; }
    }
}