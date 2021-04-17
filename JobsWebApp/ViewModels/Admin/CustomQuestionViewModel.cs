using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels.Admin
{
    public class CustomQuestionViewModel
    {
        public int DisplayOrder { get; set; } = 1;

        [Required] public string Question { get; set; }

        public bool IsRequired { get; set; } = true;
        public int? MinLength { get; set; } = 0;
        public int? MaxLength { get; set; } = 0;
    }
}