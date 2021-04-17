namespace DataLayer.BaseModels
{
    public class JobAlertSubscriptionBaseModel
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public int SalaryRangeId { get; set; }
        public int DepartmentId { get; set; }
    }
}