using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer
{
    public class ApplicationCustomQuestionAnswers
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public ApplicationCustomQuestionAnswers()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public void InsertMultiple(IEnumerable<VacancyCustomQuestionAnswerModel> questionAnswers,
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
                    VacancyCustomQuestionId = answer.VacancyApplicationId,
                    answer.Answer
                };

                _sqlDataAccess.SaveData<dynamic>(query, parameters);
            }
        }
    }
}