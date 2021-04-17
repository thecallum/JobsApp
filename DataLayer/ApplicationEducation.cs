using System.Collections.Generic;
using DataLayer.Models;
using DataLayer.CombinedModels;

namespace DataLayer
{
    public class ApplicationEducation
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public ApplicationEducation()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public void InsertMultiple(IEnumerable<VacancyEdutationModel> vacancyEducation, int vacancyApplicationId)
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
                    EducationTypeId = education.EducationTypeID,
                    education.Description
                };

                _sqlDataAccess.SaveData<dynamic>(query, parameters);
            }
        }

        public List<VacancyApplicationEducationCombinedModel> FindAll(int applicationId)
        {
            const string query = @"select t.Name as Type, e.Description
                from dbo.VacancyEducation e
                left join dbo.EducationType t on e.EducationTypeId = t.Id 
                where e.VacancyApplicationId = @ApplicationId";

            var parameters = new
            {
                ApplicationId = applicationId
            };

            var result = _sqlDataAccess.LoadData<VacancyApplicationEducationCombinedModel, dynamic>(query, parameters);

            return result;
        }
    }
}