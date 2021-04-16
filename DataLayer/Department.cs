using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Department
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public Department()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public List<DepartmentModel> FindAll()
        {
            const string query = "select Id, DepartmentName from dbo.Department;";

            var response = _sqlDataAccess.LoadData<DepartmentModel, dynamic>(query, new { });

            return response;
        }

        public DepartmentModel Find(int departmentId)
        {
            const string query = "select * from dbo.Department where Id = @Id";

            var parameters = new
            {
                Id = departmentId
            };

            var response = _sqlDataAccess.LoadData<DepartmentModel, dynamic>(query, parameters);

            return response.FirstOrDefault();
        }
    }
}
