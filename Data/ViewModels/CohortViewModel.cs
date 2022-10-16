using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class CohortViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Registration Open")]
        public bool IsRegistrationOpen { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
