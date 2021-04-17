using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class ApplicationCustomQuestionAnswersCrud : BaseCrud
    {
        public async Task InsertMultiple(IEnumerable<VacancyQuestionAnswerBaseModel> questionAnswers,
            int vacancyApplicationId)
        {
            const string query = @"insert into dbo.VacancyCustomQuestionAnswer
                (VacancyApplicationId, VacancyCustomQuestionId, Answer)
                values (@VacancyApplicationId, @VacancyCustomQuestionId, @Answer);";

            foreach (var answer in questionAnswers)
            {
                var parameters = new
                {
                    VacancyApplicationId = vacancyApplicationId,
                    answer.VacancyCustomQuestionId,
                    answer.Answer
                };

                await SqlDataAccess.SaveData<dynamic>(query, parameters);
            }
        }
    }
}