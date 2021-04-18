using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels.Apply
{
    public class VacancyCustomQuestionAnswerViewModel
    {
        [Required] 
        [MaxLength(4000)] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Answer { get; set; }

        public int QuestionId { get; set; }
    }
}