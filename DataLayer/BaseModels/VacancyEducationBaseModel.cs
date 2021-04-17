namespace DataLayer.BaseModels
{
    public class VacancyEducationBaseModel
    {
        public int Id { get; set; }
        public int VacancyApplicationId { get; set; }
        public int EducationTypeId { get; set; }
        public string Description { get; set; }
    }
}