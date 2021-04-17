using System.Collections.Generic;
using DataLayer.BaseModels;

namespace DataLayer.FullModels
{
    public class FullVacancyApplicationModel
    {
        public VacancyApplicationBaseModel VacancyApplication { get; set; }
        public List<FullVacancyEducationModel> Education { get; set; }
        public List<WorkHistoryBaseModel> WorkHistory { get; set; }
        public List<FullVacancyQuestionAnswerModel> QuestionAnswers { get; set; }
    }
}