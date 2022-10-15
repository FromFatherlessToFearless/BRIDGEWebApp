using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.Models
{
    public class ParticipantSurvey
    {
        [Key]
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public int ParticipantId { get; set; }

        public Survey Survey { get; set; }

        public Participant Participant { get; set; }
    }
}
