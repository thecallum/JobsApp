using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.FullModels;

namespace DataLayer.Crud
{
    public class FullVacancyEducationCrud : BaseCrud
    {
        public async Task<List<FullVacancyEducationModel>> FindAll(int applicationId)
        {
            const string query = @"select t.Name as Type, e.Description
                from dbo.VacancyEducation e
                left join dbo.EducationType t on e.EducationTypeId = t.Id 
                where e.VacancyApplicationId = @ApplicationId";

            var parameters = new
            {
                ApplicationId = applicationId
            };

            var result = await SqlDataAccess.LoadData<FullVacancyEducationModel, dynamic>(query, parameters);

            return result;
        }
    }
}