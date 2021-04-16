using System.Collections.Generic;
using DataLayer.Models;

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
            IEnumerable<VacancyCustomQuestionAnswerModel> questionAnswers)
        {
            const string query = @"insert into dbo.VacancyApplication
                (VacancyId, AddressLine1, AddressLine2, AddressLine3, AddressLine4, PostCode, EmailAddress, PhoneNumber)
                OUTPUT Inserted.ID
                values (@VacancyId, @AddressLine1, @AddressLine2, @AddressLine3, @AddressLine4, @PostCode, @EmailAddress, @PhoneNumber);
            ";

            var parameters1 = new
            {
                vacancyApplication.VacancyId,
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
    }
}