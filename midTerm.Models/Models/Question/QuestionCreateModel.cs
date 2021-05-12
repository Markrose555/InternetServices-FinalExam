using System.ComponentModel.DataAnnotations;

namespace midTerm.Models.Models.Question
{
    public class QuestionCreateModel
    {
        [Required(ErrorMessage = "Text for the Question is required")]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required(ErrorMessage = "A description for the Question is required")]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
