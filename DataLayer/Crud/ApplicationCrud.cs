using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class ApplicationCrud : BaseCrud
    {
        public async Task InsertApplication(VacancyApplicationBaseModel vacancyApplication,
            IEnumerable<VacancyEducationBaseModel> vacancyEducation,
            IEnumerable<WorkHistoryBaseModel> vacancyWorkHistory,
            IEnumerable<VacancyQuestionAnswerBaseModel> questionAnswers)
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

            var vacancyApplicationId = await SqlDataAccess.SaveData<dynamic>(query, parameters1, true);

            var applicationWorkHistory = new ApplicationWorkHistoryCrud();
            var applicationEducation = new ApplicationEducationCrud();
            var applicationCustomQuestionAnswers = new ApplicationCustomQuestionAnswersCrud();

            await applicationEducation.InsertMultiple(vacancyEducation, vacancyApplicationId);
            await applicationWorkHistory.InsertMultiple(vacancyWorkHistory, vacancyApplicationId);
            await applicationCustomQuestionAnswers.InsertMultiple(questionAnswers, vacancyApplicationId);
        }

        public async Task<List<VacancyApplicationBaseModel>> FindAll(int vacancyId)
        {
            const string query = @"select * from dbo.VacancyApplication
                where VacancyId = @VacancyId;";

            var parameters = new
            {
                VacancyId = vacancyId
            };

            var result = await SqlDataAccess.LoadData<VacancyApplicationBaseModel, dynamic>(query, parameters);

            return result;
        }

        public async Task<VacancyApplicationBaseModel> Find(int vacancyId, int applicantId)
        {
            const string query = @"select * from dbo.VacancyApplication
            where VacancyId = @VacancyId and Id = @ApplicantId;";

            var parameters = new
            {
                VacancyId = vacancyId,
                ApplicantId = applicantId
            };

            var result = await SqlDataAccess.LoadData<VacancyApplicationBaseModel, dynamic>(query, parameters);

            return result.First();
        }

        public async Task<int> Count(int vacancyId)
        {
            const string query = @"select COUNT(Id) from dbo.VacancyApplication
                where VacancyId = @VacancyId;";

            var parameters = new
            {
                VacancyId = vacancyId
            };

            var result = await SqlDataAccess.LoadData<int, dynamic>(query, parameters);

            return result.First();
        }
    }
}