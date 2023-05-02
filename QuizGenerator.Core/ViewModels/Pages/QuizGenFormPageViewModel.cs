using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public QuizGenFormPageViewModel()
        {
            AddNewQuestionCommand = new RelayCommand(AddNewQuestion);
            AddNewQuizCommand = new RelayCommand(addNewQuiz);
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

            DataBaseLocator.Database.Questions.Add(new Question
            {
                Id = newQuestion.Id,
                QuestionText = newQuestion.QuestionText,
                OptionA = newQuestion.OptionA,
                OptionB = newQuestion.OptionB,
                OptionC = newQuestion.OptionC,
                OptionD = newQuestion.OptionD,
                CorrectOption = newQuestion.CorrectOption
            });

      //      DataBaseLocator.Database.SaveChanges();

            QuestionText = String.Empty;
            AnswerA = String.Empty;
            AnswerB = String.Empty;
            AnswerC = String.Empty;
            AnswerD = String.Empty;
            

        }

        private void addNewQuiz()
        {
            var newQuiz = new QuizViewModel
            {
                Questions = QuestionsList.ToList(),
                QuizTitle = QuizName
            };
            QuizzesList.Add(newQuiz);

            var QuizEntityQuestions = new List<Question>();

            foreach (var question in newQuiz.Questions)
            {
                QuizEntityQuestions.Add(new Question // mapowanie, mozna by to wrzcic gdzies do innej klasy
                {
                    Id= question.Id,
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectOption = question.CorrectOption
                });
            }

            DataBaseLocator.Database.Quizzes.Add(new Quiz
            {
                Id = newQuiz.Id,
                Name = newQuiz.QuizTitle,
                Questions = QuizEntityQuestions
            });

            DataBaseLocator.Database.SaveChanges();

        }


    }
}
