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

            var query = "select * from dbo.Vacancy";

            if (salaryRange != null && department != null)
                query += " where SalaryRangeId = @SalaryRangeId and DepartmentId = @DepartmentId";
            else if (salaryRange != null)
                query += " where SalaryRangeId = @SalaryRangeId";
            else if (department != null) query += " where DepartmentId = @DepartmentId";

            query += @" Order by id
                offset @Offset rows
                fetch next @PageSize rows only;";

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
            const string query = @"select Id, JobTitle, JobDescription from dbo.Vacancy;";

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
    }
}