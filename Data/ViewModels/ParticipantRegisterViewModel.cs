using BRIDGEWebApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class ParticipantRegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\d+", ErrorMessage = "PIN can only be numbers")]
        public string PIN { get; set; }

        [Required]
        [Compare("PIN", ErrorMessage = "PIN does not match")]
        public string PINRepeat { get; set; }

        [EmailAddress]        
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
