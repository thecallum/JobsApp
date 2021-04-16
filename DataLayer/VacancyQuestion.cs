using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer
{
    public class VacancyQuestion
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public VacancyQuestion()
        {
            _sqlDataAccess = new SqlDataAccess();
        }

        public List<VacancyCustomQuestionModel> FindAll()
        {
            const string query = @"select * from dbo.VacancyCustomQuestion
                where VacancyId = @VacancyId
                order by DisplayOrder;";

            var result = _sqlDataAccess.LoadData<VacancyCustomQuestionModel, dynamic>(query, new {VacancyId = 1});

            return result;
        }

        public void InsertMultiple(List<VacancyCustomQuestionModel> questions, int vacancyId)
        {
            const string query = @"insert into dbo.VacancyCustomQuestion
            (VacancyId, Question, IsRequired, MinLength, MaxLength, DisplayOrder)
            values (@VacancyId, @Question, @IsRequired, @MinLength, @MaxLength, @DisplayOrder);
            ";

            foreach(var question in questions)
            {
                var parameters = new
                {
                    VacancyId = vacancyId,
                    Question = question.Question,
                    IsRequired = question.IsRequired,
                    MinLength = question.MinLength,
                    MaxLength = question.MaxLength,
                    DisplayOrder = question.DisplayOrder
                };

                _sqlDataAccess.SaveData<dynamic>(query, parameters, false);
            }
        }
    }
}