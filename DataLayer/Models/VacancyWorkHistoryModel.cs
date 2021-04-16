using System;

namespace DataLayer.Models
{
    public class VacancyWorkHistoryModel
    {
        public int Id { get; set; }
        public int VacancyApplicationId { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Summary { get; set; }
    }
}