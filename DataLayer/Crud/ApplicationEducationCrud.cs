using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class ApplicationEducationCrud : BaseCrud
    {
        public async Task InsertMultiple(IEnumerable<VacancyEducationBaseModel> vacancyEducation,
            int vacancyApplicationId)
        {
            const string query = @"insert into dbo.VacancyEducation
                (VacancyApplicationId, EducationTypeId, Description)
                values (@VacancyApplicationId, @EducationTypeId, @Description);
            ";

            foreach (var education in vacancyEducation)
            {
                var parameters = new
                {
                    VacancyApplicationId = vacancyApplicationId,
                    EducationTypeId = education.EducationTypeId,
                    education.Description
                };

                await SqlDataAccess.SaveData<dynamic>(query, parameters);
            }
        }
    }
}