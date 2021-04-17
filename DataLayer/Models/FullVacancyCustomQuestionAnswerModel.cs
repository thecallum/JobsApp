namespace DataLayer.Models
{
    public class FullVacancyCustomQuestionAnswerModel
    {
        public int Id { get; set; }
        public int VacancyApplicationId { get; set; }
        public int VacancyCustomQuestionId { get; set; }
        public string Answer { get; set; }
    }
}