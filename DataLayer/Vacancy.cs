using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;

namespace DataLayer
{
    public class Vacancy
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public Vacancy()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public List<VacancyModel> FindWithFilters(int page = 1, int? salaryRange = null, int? department = null)
        {
            const int pageSize = 10;

            string query;

            if (salaryRange != null && department != null)
                query = @"select * from dbo.Vacancy
                    where SalaryRangeId = @SalaryRangeId and DepartmentId = @DepartmentId and Published = 1
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";
            else if (salaryRange != null)
                query = @"select * from dbo.Vacancy
                    where SalaryRangeId = @SalaryRangeId and Published = 1
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";
            else if (department != null)
            {
                query = @"select * from dbo.Vacancy
                    where DepartmentId = @DepartmentId and Published = 1
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";

            } else
            {
                query = @"select * from dbo.Vacancy
                    where Published = 1
                    Order by id
                    offset @Offset rows
                    fetch next @PageSize rows only;
                ";
            }


            var parameters = new
            {
                SalaryRangeId = salaryRange,
                DepartmentId = department,
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            };

            var response = _sqlDataAccess.LoadData<VacancyModel, dynamic>(query, parameters);


            return response;
        }

        public VacancyModel FindById(int id)
        {
            const string query = @"select * from dbo.Vacancy
                where Id = @Id;";

            var response = _sqlDataAccess.LoadData<VacancyModel, dynamic>(query, new {Id = id});

            return response.FirstOrDefault();
        }

        public List<VacancyModel> FindAll()
        {
            const string query = @"select * from dbo.Vacancy;";

            var response = _sqlDataAccess.LoadData<VacancyModel, dynamic>(query, new { });

            return response;
        }

        public List<VacancyModel> FindAllPublished()
        {
            const string query = @"select * from dbo.Vacancy where Published = 1;";

            var response = _sqlDataAccess.LoadData<VacancyModel, dynamic>(query, new { });

            return response;
        }

        public List<VacancyModel> FindAllDraft()
        {
            const string query = @"select * from dbo.Vacancy where Published = 0;";

            var response = _sqlDataAccess.LoadData<VacancyModel, dynamic>(query, new { });

            return response;
        }



        public int Insert(VacancyModel vacancy, List<VacancyCustomQuestionModel> questions)
        {
            var query = @"insert into dbo.Vacancy 
                (JobTitle, JobDescription, SalaryMin, SalaryMax, SalaryRangeId, DepartmentId, ContractType, StartDate, EndDate, Published)
                output Inserted.ID
                values (@JobTitle, @JobDescription, @SalaryMin, @SalaryMax, @SalaryRangeId, @DepartmentId, @ContractType, @StartDate, @EndDate, @Published);
            ";

            var parameters = new
            {
                JobTitle = vacancy.JobTitle,
                JobDescription = vacancy.JobDescription,
                SalaryMin = vacancy.SalaryMin,
                SalaryMax = vacancy.SalaryMax,
                SalaryRangeId = vacancy.SalaryRangeId,
                DepartmentId = vacancy.DepartmentId,
                ContractType = vacancy.ContractType,
                StartDate = vacancy.StartDate,
                EndDate = vacancy.EndDate,
                Published = vacancy.Published
            };

            var vacancyId =_sqlDataAccess.SaveData<dynamic>(query, parameters, true);

            var vacancyQuestionCrud = new VacancyQuestion();
            vacancyQuestionCrud.InsertMultiple(questions, vacancyId);

            return vacancyId;
        }

        public void Publish(int vacancyId, bool published)
        {
            const string query = @"update dbo.Vacancy 
                set Published = @Published
                where Id = @VacancyId; ";

            var parameters = new
            {
                Published = published ? 1 : 0,
                VacancyId = vacancyId
            };

            _sqlDataAccess.UpdateData<dynamic>(query, parameters);

        }
    }
}