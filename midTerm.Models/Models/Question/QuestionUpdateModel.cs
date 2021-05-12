using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using midTerm.Models.Models.Option;

namespace midTerm.Models.Models.Question
{
    public class QuestionUpdateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Text for the Question is required")]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required(ErrorMessage = "A description for the Question is required")]
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public ICollection<OptionBaseModel> Options { get; set; }
    }
}
