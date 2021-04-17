using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class ApplicationWorkHistoryCrud : BaseCrud
    {
        public async Task InsertMultiple(IEnumerable<WorkHistoryBaseModel> vacancyWorkHistory, int vacancyApplicationId)
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

                await SqlDataAccess.SaveData<dynamic>(query, parameters);
            }
        }

        public async Task<List<WorkHistoryBaseModel>> FindAll(int vacancyId)
        {
            const string query = @"select JobTitle, EmployerName, Summary, StartDate, EndDate
                from dbo.VacancyWorkHistory
                where VacancyApplicationId = @VacancyId;";

            var parameters = new
            {
                VacancyId = vacancyId
            };

            var result = await SqlDataAccess.LoadData<WorkHistoryBaseModel, dynamic>(query, parameters);
            return result;
        }
    }
}