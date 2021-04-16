using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class VacancyApplicationModel
    {
        public int Id { get; set; }

        public int VacancyId { get; set; }

        [Required] [MaxLength(50)] public string AddressLine1 { get; set; }

        [MaxLength(50)] public string AddressLine2 { get; set; }

        [MaxLength(50)] public string AddressLine3 { get; set; }

        [MaxLength(50)] public string AddressLine4 { get; set; }

        [Required] [MaxLength(50)] public string PostCode { get; set; }

        [Required] [MaxLength(50)] public string PhoneNumber { get; set; }

        [Required] [MaxLength(50)] public string EmailAddress { get; set; }
    }
}