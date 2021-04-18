using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels.Apply
{
    public class VacancyApplicationViewModel
    {
        public int Id { get; set; }

        public int VacancyId { get; set; }

        [Required] 
        [MaxLength(50)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required] 
        [MaxLength(50)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required] 
        [MaxLength(50)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [MaxLength(50)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [MaxLength(50)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "Address Line 3")]
        public string AddressLine3 { get; set; }

        [MaxLength(50)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "Address Line 4")]
        public string AddressLine4 { get; set; }

        [Required] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [MaxLength(50)] 
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required] 
        [MaxLength(50)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required] 
        [MaxLength(100)] 
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

       
    }
}