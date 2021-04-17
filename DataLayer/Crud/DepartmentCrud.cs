using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class DepartmentCrud : BaseCrud
    {
        public async Task<IEnumerable<DepartmentBaseModel>> FindAll()
        {
            const string query = "select Id, DepartmentName from dbo.Department;";

            var response = await SqlDataAccess.LoadData<DepartmentBaseModel, dynamic>(query, new { });

            return response;
        }

        public async Task<DepartmentBaseModel> Find(int departmentId)
        {
            const string query = "select * from dbo.Department where Id = @Id";

            var parameters = new
            {
                Id = departmentId
            };

            var response = await SqlDataAccess.LoadData<DepartmentBaseModel, dynamic>(query, parameters);

            return response.FirstOrDefault();
        }
    }
}