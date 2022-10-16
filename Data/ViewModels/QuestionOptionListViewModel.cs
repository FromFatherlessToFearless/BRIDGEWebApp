namespace BRIDGEWebApp.Data.ViewModels
{
    public class QuestionOptionListViewModel
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public IList<QuestionOptionViewModel> QuestionOptionViewModels { get; set; }

        public QuestionOptionListViewModel()
        {
            QuestionOptionViewModels = new List<QuestionOptionViewModel>();
        }
    }
}
