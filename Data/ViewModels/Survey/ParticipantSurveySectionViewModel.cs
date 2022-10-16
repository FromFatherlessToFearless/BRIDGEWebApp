namespace BRIDGEWebApp.Data.ViewModels.Survey
{
    public class ParticipantSurveySectionViewModel
    {
        public string SectionName { get; set; }

        public string SectionDescription { get; set; }

        public int SectionId { get; set; }

        public int Order { get; set; }

        public List<ParticipantSurveyQuestionViewModel> Questions { get; set; }
    }
}
