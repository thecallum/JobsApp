using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class EducationTypeCrud : BaseCrud
    {
        public async Task<List<EducationTypeBaseModel>> FindAll()
        {
            const string query = "select Id, Name from dbo.EducationType;";

            var response = await SqlDataAccess.LoadData<EducationTypeBaseModel, dynamic>(query, new { });

            return response;
        }
    }
}