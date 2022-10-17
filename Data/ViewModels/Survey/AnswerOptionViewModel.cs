namespace BRIDGEWebApp.Data.ViewModels.Survey
{
    public class AnswerOptionViewModel
    {
        public int QuestionOptionId { get; set; }

        public string OptionText { get; set; }

        public bool IsOther { get; set; }

        public string Value { get; set; }
    }
}
