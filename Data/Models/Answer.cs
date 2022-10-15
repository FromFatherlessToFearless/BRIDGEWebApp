using System.ComponentModel.DataAnnotations;

namespace BRIDGEWebApp.Data.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public int ParticipantSurveyId { get; set; }

        public int QuestionId { get; set; }

        [StringLength(2000)]
        public string? Text { get; set; }

        public ParticipantSurvey ParticipantSurvey { get; set; }    

        public Question Question { get; set; }  
    }
}
