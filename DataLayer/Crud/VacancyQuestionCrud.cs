using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.BaseModels;

namespace DataLayer.Crud
{
    public class VacancyQuestionCrud : BaseCrud
    {
        public async Task<List<VacancyQuestionBaseModel>> FindAll(int vacancyId)
        {
            const string query = @"select * from dbo.VacancyCustomQuestion
                where VacancyId = @VacancyId
                order by DisplayOrder;";

            var result =
                await SqlDataAccess.LoadData<VacancyQuestionBaseModel, dynamic>(query, new {VacancyId = vacancyId});

            return result;
        }

        public async Task InsertMultiple(IEnumerable<VacancyQuestionBaseModel> questions, int vacancyId)
        {
            const string query = @"insert into dbo.VacancyCustomQuestion
            (VacancyId, Question, IsRequired, MinLength, MaxLength, DisplayOrder)
            values (@VacancyId, @Question, @IsRequired, @MinLength, @MaxLength, @DisplayOrder);
            ";

            foreach (var question in questions)
            {
                var parameters = new
                {
                    VacancyId = vacancyId,
                    question.Question,
                    question.IsRequired,
                    question.MinLength,
                    question.MaxLength,
                    question.DisplayOrder
                };

                await SqlDataAccess.SaveData<dynamic>(query, parameters);
            }
        }
    }
}