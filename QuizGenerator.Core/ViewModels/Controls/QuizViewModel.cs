using System.Collections.Generic;
using QuizGenerator.Core.ViewModels.Base;

namespace QuizGenerator.Core.ViewModels.Controls
{
    public class QuizViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string QuizTitle { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public bool IsSelected { get; set; }

    }
}
