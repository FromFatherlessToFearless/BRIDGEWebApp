using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRIDGEWebApp.Data.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1000)]
        public string Text { get; set; }

        public int Order { get; set; }

        public bool IsMandatory { get; set; }

        [StringLength(100)]
        public string QuestionType { get; set; }

        public int SurveyId { get; set; }

        public int SurveySectionId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedByIdentityUser { get; set; }

        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public IdentityUser UpdatedByIdentityUser { get; set; }

        public Survey Survey { get; set; }

        public SurveySection SurveySection {get;set;}
    }
}
