using System.ComponentModel.DataAnnotations;

namespace midTerm.Models.Models.Option
{
    public class OptionUpdateModel
    {
        [Required(ErrorMessage = "An option ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Text for the option is required")]
        public string Text { get; set; }
        
        public int? Order { get; set; }
        
        [Required(ErrorMessage = "A corresponding question is required")]
        public int QuestionId { get; set; }

    }
}
