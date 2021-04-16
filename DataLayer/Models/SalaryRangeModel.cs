namespace DataLayer.Models
{
    public class SalaryRangeModel
    {
        public int Id { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }

        public string DisplayValue => $"£{MinAmount} - £{MaxAmount}";
    }
}