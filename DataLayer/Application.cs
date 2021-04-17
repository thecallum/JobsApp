using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class Application
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public Application()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public void InsertApplication(VacancyApplicationModel vacancyApplication,
            IEnumerable<VacancyEdutationModel> vacancyEducation,
            IEnumerable<VacancyWorkHistoryModel> vacancyWorkHistory,
            IEnumerable<FullVacancyCustomQuestionAnswerModel> questionAnswers)
        {
            const string query = @"insert into dbo.VacancyApplication
                (VacancyId, FirstName, LastName, AddressLine1, AddressLine2, AddressLine3, AddressLine4, PostCode, EmailAddress, PhoneNumber)
                OUTPUT Inserted.ID
                values (@VacancyId, @FirstName, @LastName, @AddressLine1, @AddressLine2, @AddressLine3, @AddressLine4, @PostCode, @EmailAddress, @PhoneNumber);
            ";

            var parameters1 = new
            {
                vacancyApplication.VacancyId,
                vacancyApplication.FirstName,
                vacancyApplication.LastName,
                vacancyApplication.AddressLine1,
                vacancyApplication.AddressLine2,
                vacancyApplication.AddressLine3,
                vacancyApplication.AddressLine4,
                vacancyApplication.PostCode,
                vacancyApplication.EmailAddress,
                vacancyApplication.PhoneNumber
            };

            var vacancyApplicationId = _sqlDataAccess.SaveData<dynamic>(query, parameters1, true);

            var applicationWorkHistory = new ApplicationWorkHistory();
            var applicationEducation = new ApplicationEducation();
            var applicationCustomQuestionAnswers = new ApplicationCustomQuestionAnswers();

            applicationEducation.InsertMultiple(vacancyEducation, vacancyApplicationId);
            applicationWorkHistory.InsertMultiple(vacancyWorkHistory, vacancyApplicationId);
            applicationCustomQuestionAnswers.InsertMultiple(questionAnswers, vacancyApplicationId);
        }

        public List<VacancyApplicationModel> FindAll(int vacancyId)
        {
            const string query = @"select * from dbo.VacancyApplication
                where VacancyId = @VacancyId;";

            var parameters = new
            {
                VacancyId = vacancyId
            };

            var result = _sqlDataAccess.LoadData<VacancyApplicationModel, dynamic>(query, parameters);

            return result;
        }

        public VacancyApplicationModel Find(int vacancyId, int applicantId)
        {
            const string query = @"select * from dbo.VacancyApplication
            where VacancyId = @VacancyId and Id = @ApplicantId;";

            var parameters = new
            {
                VacancyId = vacancyId,
                ApplicantId = applicantId
            };

            var result = _sqlDataAccess.LoadData<VacancyApplicationModel, dynamic>(query, parameters);

            return result.First();
        }

        public int Count(int vacancyId)
        {
            const string query = @"select COUNT(Id) from dbo.VacancyApplication
                where VacancyId = @VacancyId;";

            var parameters = new
            {
                VacancyId = vacancyId
            };

            var result = _sqlDataAccess.LoadData<int, dynamic>(query, parameters);

            return result.First();
        }
    }
}