using BRIDGEWebApp.Data.Enum;
using BRIDGEWebApp.Data.Models;

namespace BRIDGEWebApp.Data.ViewModels.Survey
{
    public class ParticipantSurveyQuestionViewModel
    {
        public string QuestionText { get; set; }
        public int QuestionId { get; set; }

        public bool IsMandatory { get; set; }

        public int Order { get; set; }

        public string QuestionType { get; set; }

        public List<QuestionOption> QuestionOptions { get; set; }   

    }
}
