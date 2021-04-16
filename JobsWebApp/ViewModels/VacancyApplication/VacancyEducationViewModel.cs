using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels
{
    public class VacancyEducationViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "The Education type is required")]
        public int EducationTypeId { get; set; }

        [Required] [MaxLength(400)] public string Description { get; set; }
    }
}