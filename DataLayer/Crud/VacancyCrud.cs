using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class VacancyCrud : BaseCrud
    {
        public async Task<List<VacancyBaseModel>> FindWithFilters(int page = 1, int? salaryRangeId = null,
            int? department = null)
        {
            const int pageSize = 10;


            string query;

            if (salaryRangeId != null && department != null)
            {
                query = @"select * from dbo.Vacancy
                     where
                        DepartmentId = @DepartmentId and 
                        StartDate <= CAST( GETDATE() AS Date ) and
                        EndDate >= CAST( GETDATE() AS Date ) and
                        Published = 1 and
                        (
                            (SalaryMin >= @SalaryRangeMin and SalaryMin <= @SalaryRangeMax) or
                            (SalaryMax >= @SalaryRangeMin and SalaryMax <= @SalaryRangeMax) or
                            (SalaryMin <= @SalaryRangeMin and SalaryMax >= @SalaryRangeMax)
                        )
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";
            }
            else if (salaryRangeId != null)
                query = @"select * from dbo.Vacancy
                    where 
                        StartDate <= CAST( GETDATE() AS Date ) and
                        EndDate >= CAST( GETDATE() AS Date ) and
                        Published = 1 and 
                        (
                            (SalaryMin >= @SalaryRangeMin and SalaryMin <= @SalaryRangeMax) or
                            (SalaryMax >= @SalaryRangeMin and SalaryMax <= @SalaryRangeMax) or
                            (SalaryMin <= @SalaryRangeMin and SalaryMax >= @SalaryRangeMax)
                        )
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";
            else if (department != null)
                query = @"select * from dbo.Vacancy
                    where 
                        DepartmentId = @DepartmentId and 
                        StartDate <= CAST( GETDATE() AS Date ) and
                        EndDate >= CAST( GETDATE() AS Date ) and
                        Published = 1
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";
            else
                query = @"select * from dbo.Vacancy
                    where 
                        StartDate <= CAST( GETDATE() AS Date ) and
                        EndDate >= CAST( GETDATE() AS Date ) and
                        Published = 1
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";

            int salaryRangeMin = 0; // default values (not used if not in query)
            int salaryRangeMax = 0;


            if (salaryRangeId != null)
            {
                var salaryRangeCrud = new SalaryRangeCrud();
                var salaryRange = await salaryRangeCrud.Find((int)salaryRangeId);

                salaryRangeMin = salaryRange.MinAmount;
                salaryRangeMax = salaryRange.MaxAmount;
            }

            var parameters = new
            {
                DepartmentId = department,
                Offset = (page - 1) * pageSize,
                PageSize = pageSize,

                SalaryRangeMin = salaryRangeMin,
                SalaryRangeMax = salaryRangeMax
            };

           

            var response = await SqlDataAccess.LoadData<VacancyBaseModel, dynamic>(query, parameters);

            return response;
        }

        public async Task<VacancyBaseModel> FindById(int id)
        {
            const string query = @"select * from dbo.Vacancy
                where Id = @Id;";

            var response = await SqlDataAccess.LoadData<VacancyBaseModel, dynamic>(query, new {Id = id});

            return response.FirstOrDefault();
        }

        public async Task<List<VacancyBaseModel>> FindAll()
        {
            const string query = @"select * from dbo.Vacancy;";

            var response = await SqlDataAccess.LoadData<VacancyBaseModel, dynamic>(query, new { });

            return response;
        }

        public async Task<List<VacancyBaseModel>> FindAllPublished()
        {
            const string query = @"select * from dbo.Vacancy where Published = 1;";

            var response = await SqlDataAccess.LoadData<VacancyBaseModel, dynamic>(query, new { });

            return response;
        }

        public async Task<List<VacancyBaseModel>> FindAllDraft()
        {
            const string query = @"select * from dbo.Vacancy where Published = 0;";

            var response = await SqlDataAccess.LoadData<VacancyBaseModel, dynamic>(query, new { });

            return response;
        }

        public async Task<int> Insert(VacancyBaseModel vacancy, IEnumerable<VacancyQuestionBaseModel> questions)
        {
            const string query = @"insert into dbo.Vacancy 
                (JobTitle, JobDescription, SalaryMin, SalaryMax, DepartmentId, ContractType, StartDate, EndDate, Published)
                output Inserted.ID
                values (@JobTitle, @JobDescription, @SalaryMin, @SalaryMax, @DepartmentId, @ContractType, @StartDate, @EndDate, @Published);
            ";

            var parameters = new
            {
                vacancy.JobTitle,
                vacancy.JobDescription,
                vacancy.SalaryMin,
                vacancy.SalaryMax,
                vacancy.DepartmentId,
                vacancy.ContractType,
                vacancy.StartDate,
                vacancy.EndDate,
                vacancy.Published
            };

            var vacancyId = await SqlDataAccess.SaveData<dynamic>(query, parameters, true);

            var vacancyQuestionCrud = new VacancyQuestionCrud();
            await vacancyQuestionCrud.InsertMultiple(questions, vacancyId);

            return vacancyId;
        }

        public async Task Publish(int vacancyId, bool published)
        {
            const string query = @"update dbo.Vacancy 
                set Published = @Published
                where Id = @VacancyId; ";

            var parameters = new
            {
                Published = published ? 1 : 0,
                VacancyId = vacancyId
            };

            await SqlDataAccess.UpdateData<dynamic>(query, parameters);
        }
    }
}