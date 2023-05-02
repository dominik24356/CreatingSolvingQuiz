using System.ComponentModel.DataAnnotations.Schema;

namespace QuizGenerator.Database.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public string OptionD { get; set; }

        public string CorrectOption { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}
