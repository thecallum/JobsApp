namespace DataLayer.Models
{
    public class VacancyEdutationModel
    {
        public int Id { get; set; }
        public int VacancyApplicationId { get; set; }
        public int EducationTypeID { get; set; }
        public string Description { get; set; }
    }
}