using System.Threading.Tasks;
using DataLayer.Crud;
using DataLayer.FullModels;

namespace DataLayer.CombinedCrud
{
    public class FullVacancyApplicantCrud
    {
        public async Task<FullVacancyApplicationModel> Find(int vacancyId, int applicantId)
        {
            var applicationCrud = new ApplicationCrud();
            var educationCrud = new FullVacancyEducationCrud();
            var workHistoryCrud = new ApplicationWorkHistoryCrud();

            var questionAnswerCrud = new FullVacancyQuestionAnswerCrud();


            var applicant = new FullVacancyApplicationModel
            {
                VacancyApplication = await applicationCrud.Find(vacancyId, applicantId),
                QuestionAnswers = await questionAnswerCrud.FindAll(vacancyId, applicantId),
                Education = await educationCrud.FindAll(applicantId),
                WorkHistory = await workHistoryCrud.FindAll(applicantId)
            };

            return applicant;
        }
    }
}