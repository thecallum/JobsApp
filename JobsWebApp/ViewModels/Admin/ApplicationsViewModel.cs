using DataLayer.CombinedModels;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsWebApp.ViewModels.Admin
{
    public class ApplicationsViewModel
    {
        public VacancyModel Vacancy { get; set; }

        public List<VacancyApplicationModel> Applications { get; set; }

        public FullVacancyApplicationModel Applicant { get; set; }
    }

    public class FullVacancyApplicationModel
    {
        public VacancyApplicationModel VacancyApplication { get; set; }
        public List<VacancyApplicationEducationCombinedModel> Education { get; set; }
        public List<VacancyWorkHistoryModel> WorkHistory { get; set; }
        public List<ApplicationQuestionAnswerModel> QuestionAnswers { get; set; }
    }
}
