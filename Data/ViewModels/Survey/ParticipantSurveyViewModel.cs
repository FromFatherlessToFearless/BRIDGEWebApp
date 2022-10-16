namespace BRIDGEWebApp.Data.ViewModels.Survey
{
    public class ParticipantSurveyViewModel
    {
        public string CohortName { get; set; }

        public int CohortId { get; set; }

        public int SurveyId { get; set; }

        public string SurveyName { get; set; }

        public List<ParticipantSurveySectionViewModel> ParticipantSurveySections { get; set; }
    }
}
