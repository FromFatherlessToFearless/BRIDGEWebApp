using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class SurveyViewModel
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

    }
}
