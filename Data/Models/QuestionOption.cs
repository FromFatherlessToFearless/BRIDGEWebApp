using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRIDGEWebApp.Data.Models
{
    public class QuestionOption
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Text { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Order { get; set; }

        [Display(Name = "Is Other")]
        public bool IsOther { get; set; }

        public int QuestionId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedByIdentityUser { get; set; }

        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public IdentityUser UpdatedByIdentityUser { get; set; }

        public Question Question { get; set; }  
    }
}
