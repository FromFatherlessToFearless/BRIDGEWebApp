using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class QuestionViewModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1000)]
        public string Text { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Order { get; set; }

        public bool IsMandatory { get; set; }

        [StringLength(100)]
        public string QuestionType { get; set; }

        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
 
        public int SurveySectionId { get; set; }
        public string SurveySectionName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
