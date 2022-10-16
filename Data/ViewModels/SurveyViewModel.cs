using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class SurveyViewModel
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name="Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

    }
}
