using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRIDGEWebApp.Data.Models
{
    public class Cohort
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Is Registration Open")]
        public bool IsRegistrationOpen { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedByIdentityUser { get; set; }

        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public IdentityUser UpdatedByIdentityUser { get; set; }

        public List<Participant> Participants { get; set; }
    }
}
