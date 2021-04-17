using System.ComponentModel.DataAnnotations;

namespace JobsWebApp.ViewModels.Apply
{
    public class VacancyCustomQuestionAnswerViewModel
    {
        [Required] [MaxLength(400)] public string Answer { get; set; }

        public int QuestionId { get; set; }
    }
}