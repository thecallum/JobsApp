namespace DataLayer.BaseModels
{
    public class VacancyQuestionBaseModel
    {
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public int DisplayOrder { get; set; }
        public string Question { get; set; }
        public bool IsRequired { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
    }
}