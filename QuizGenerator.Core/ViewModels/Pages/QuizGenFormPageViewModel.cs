using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using QuizGenerator.Core.Helpers;
using QuizGenerator.Core.ViewModels.Base;
using QuizGenerator.Core.ViewModels.Controls;
using QuizGenerator.Database.Entities;

namespace QuizGenerator.Core.ViewModels.Pages
{
    public class QuizGenFormPageViewModel : BaseViewModel
    {
        public ObservableCollection<QuizViewModel> QuizzesList { get; set; } = new ObservableCollection<QuizViewModel>();

        public ObservableCollection<QuestionViewModel> QuestionsList { get; set; } = new ObservableCollection<QuestionViewModel>();

        public string QuizName { get; set; }
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string ProperAnswer { get; set; }

        public ICommand AddNewQuestionCommand { get; set; }
        public ICommand AddNewQuizCommand { get; set; }
        //public ICommand SaveQuizCommand { get; set; }

        public QuizGenFormPageViewModel()
        {
            AddNewQuestionCommand = new RelayCommand(AddNewQuestion);
            AddNewQuizCommand = new RelayCommand(AddNewQuiz);
          //  SaveQuizCommand = new RelayCommand(SaveQuiz);
        }

        private void AddNewQuestion()
        {
            var newQuestion = new QuestionViewModel
            {
                QuestionText = this.QuestionText,
                OptionA = AnswerA,
                OptionB = AnswerB,
                OptionC = AnswerC,
                OptionD = AnswerD,
                CorrectOption = ProperAnswer
            };
            QuestionsList.Add(newQuestion);

            QuestionText = string.Empty;
            AnswerA = string.Empty;
            AnswerB = string.Empty;
            AnswerC = string.Empty;
            AnswerD = string.Empty;
            ProperAnswer = string.Empty;
        }

        private void AddNewQuiz()
        {

            var newQuiz = new QuizViewModel
            {
                Questions = QuestionsList.ToList(),
                QuizTitle = QuizName
            };
            QuizzesList.Add(newQuiz);

            SaveQuiz();

            QuizName = string.Empty;
            QuestionsList.Clear();

           

        }


        private void SaveQuiz()
        {
            var quizEntity = new Quiz
            {
                Name = QuizzesList.Last().QuizTitle,
                Questions = new List<Question>()
           
            };

            foreach (var question in QuizzesList.Last().Questions)
            {
                quizEntity.Questions.Add(new Question
                {
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectOption = question.CorrectOption
                });
            }

        

            DataBaseLocator.Database.Quizzes.Add(quizEntity);


             foreach (var question in quizEntity.Questions)
            {
                question.QuizId = quizEntity.Id;
            }

            DataBaseLocator.Database.SaveChanges();

           

            DataBaseLocator.Database.SaveChanges();
        }
    }
}
