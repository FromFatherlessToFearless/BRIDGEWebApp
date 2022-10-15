namespace BRIDGEWebApp.Data.Models
{
    public class AnswerOption
    {
        public int Id { get; set; }

        public int AnswerId { get; set; }

        public int QuestionOptionId { get; set; }

        public Answer Answer { get; set; }

        public QuestionOption QuestionOption  { get; set; }
    }
}
