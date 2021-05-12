using System.ComponentModel.DataAnnotations.Schema;

namespace midTerm.Data.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Order { get; set; }
        
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}