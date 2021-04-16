using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer
{
    public class EducationType
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public EducationType()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public List<EducationTypeModel> FindAll()
        {
            const string query = "select Id, Name from dbo.EducationType;";

            var response = _sqlDataAccess.LoadData<EducationTypeModel, dynamic>(query, new { });

            return response;
        }
    }
}