using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer
{
    public class ApplicationWorkHistory
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public ApplicationWorkHistory()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public void InsertMultiple(IEnumerable<VacancyWorkHistoryModel> vacancyWorkHistory, int vacancyApplicationId)
        {
            const string query = @"insert into dbo.VacancyWorkHistory
                (VacancyApplicationId, EmployerName, JobTitle, Summary, StartDate, EndDate)
                values (@VacancyApplicationId, @EmployerName, @JobTitle, @Summary, @StartDate, @EndDate);
            ";

            foreach (var workHistory in vacancyWorkHistory)
            {
                var parameters = new
                {
                    VacancyApplicationId = vacancyApplicationId,
                    workHistory.EmployerName,
                    workHistory.JobTitle,
                    workHistory.Summary,
                    workHistory.StartDate,
                    workHistory.EndDate
                };

                _sqlDataAccess.SaveData<dynamic>(query, parameters);
            }
        }
    }
}