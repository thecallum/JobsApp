using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class SalaryRangeCrud : BaseCrud
    {
        public async Task<IEnumerable<SalaryRangeBaseModel>> FindAll()
        {
            const string query = "select Id, MinAmount, MaxAmount from dbo.SalaryRange;";

            var response = await SqlDataAccess.LoadData<SalaryRangeBaseModel, dynamic>(query, new { });

            return response;
        }
    }
}