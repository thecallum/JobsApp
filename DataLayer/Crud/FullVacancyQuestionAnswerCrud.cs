using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.FullModels;

namespace DataLayer.Crud
{
    public class FullVacancyQuestionAnswerCrud : BaseCrud
    {
        public async Task<List<FullVacancyQuestionAnswerModel>> FindAll(int vacancyId, int applicationId)
        {
            const string query = @"select q.Question, a.Answer
                    from dbo.VacancyCustomQuestion q
                    Left Join dbo.VacancyCustomQuestionAnswer a
                    on q.Id = a.VacancyCustomQuestionId
                    where q.VacancyId = @VacancyId and a.VacancyApplicationId = @ApplicationId;
                    ";

            var parameters = new
            {
                VacancyId = vacancyId,
                ApplicationId = applicationId
            };

            var response = await SqlDataAccess.LoadData<FullVacancyQuestionAnswerModel, dynamic>(query, parameters);

            return response;
        }
    }
}