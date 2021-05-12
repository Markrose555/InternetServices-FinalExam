using System.Collections.Generic;

namespace midTerm.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Option> Options { get; set; }
    }
}