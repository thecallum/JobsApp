using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SalaryRange
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public SalaryRange()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public List<SalaryRangeModel> FindAll()
        {
            const string query = "select Id, MinAmount, MaxAmount from dbo.SalaryRange;";

            var response = _sqlDataAccess.LoadData<SalaryRangeModel, dynamic>(query, new { });

            return response;
        }
    }
}
