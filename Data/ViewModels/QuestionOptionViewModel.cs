using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class QuestionOptionViewModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Text { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Order { get; set; }

        public bool IsOther { get; set; }

        public int QuestionId { get; set; }
  
        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
