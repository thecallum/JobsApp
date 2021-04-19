using System;

namespace DataLayer.BaseModels
{
    public class VacancyBaseModel
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public int SalaryMin { get; set; }
        public int SalaryMax { get; set; }
        public int DepartmentId { get; set; }
        public string ContractType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Published { get; set; }
    }
}