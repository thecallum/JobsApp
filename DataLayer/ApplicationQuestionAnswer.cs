using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ApplicationQuestionAnswer
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public ApplicationQuestionAnswer()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public List<ApplicationQuestionAnswerModel> FindAll(int vacancyId, int applicationId)
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

            var response = _sqlDataAccess.LoadData<ApplicationQuestionAnswerModel, dynamic>(query, parameters);

            return response;
        }
    }
}
