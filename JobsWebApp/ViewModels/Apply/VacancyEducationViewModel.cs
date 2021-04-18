using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels.Apply
{
    public class VacancyEducationViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "The Education type is required")]
        public int EducationTypeId { get; set; }

        [Required] 
        [MaxLength(10)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Description { get; set; }
    }
}