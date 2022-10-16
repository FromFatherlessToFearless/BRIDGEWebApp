namespace BRIDGEWebApp.Data.ViewModels
{
    public class QuestionListViewModel
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public IList<QuestionViewModel> QuestionViewModels { get; set; }

        public QuestionListViewModel()
        {
            QuestionViewModels = new List<QuestionViewModel>();
        }
    }
}
