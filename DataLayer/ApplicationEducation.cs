using System.Collections.Generic;
using DataLayer.Models;

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
    }
}