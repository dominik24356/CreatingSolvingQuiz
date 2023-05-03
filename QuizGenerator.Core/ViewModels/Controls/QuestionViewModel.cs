using QuizGenerator.Core.ViewModels.Base;

namespace QuizGenerator.Core.ViewModels.Controls
{
    public class QuestionViewModel : BaseViewModel
    {

        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }

    }
}
