using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class SurveySectionViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(1000)]
        [Required]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Order { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
