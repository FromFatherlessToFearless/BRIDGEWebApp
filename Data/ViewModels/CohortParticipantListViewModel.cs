using BRIDGEWebApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.ViewModels
{
    public class CohortParticipantListModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsRegistrationOpen { get; set; }

        public IList<Participant> Participants { get; set; }
    }
}
