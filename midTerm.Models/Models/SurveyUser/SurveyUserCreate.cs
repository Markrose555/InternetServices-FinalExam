using System;
using System.ComponentModel.DataAnnotations;
using midTerm.Data.Enums;

namespace midTerm.Models.Models.SurveyUser
{
    public class SurveyUserCreate
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? DoB { get; set; }

        [Required]
        public Gender Gender { get; set; }
        
        public string Country { get; set; }

    }
}