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
        public ICommand DeleteSelectedQuestionsCommand { get; set; }

        public ICommand DeleteSelectedQuizCommand { get; set; }
       
        public QuizGenFormPageViewModel()
        {
            AddNewQuestionCommand = new RelayCommand(AddNewQuestion);
            AddNewQuizCommand = new RelayCommand(AddNewQuiz);
            DeleteSelectedQuestionsCommand = new RelayCommand(deleteSelectedQuestions);
            DeleteSelectedQuizCommand = new RelayCommand(deleteSelectedQuiz);
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

            Quiz savedQuiz = SaveQuiz();

            newQuiz.Id = savedQuiz.Id;

            QuizzesList.Add(newQuiz);

            QuizName = string.Empty;
            QuestionsList.Clear();
        }


        private Quiz SaveQuiz()
        {
            var quizEntity = new Quiz
            {
                //Name = QuizzesList.Last().QuizTitle,
                Name = QuizName,
                Questions = new List<Question>()
           
            };

            foreach (var question in QuestionsList)
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
            DataBaseLocator.Database.SaveChanges();


            return quizEntity;

        }

        private void deleteSelectedQuestions()
        {
            var selectedQuestions = QuestionsList.Where(q => q.IsSelected).ToList();

            foreach (var question in selectedQuestions)
            {
                QuestionsList.Remove(question);

                // Remove question from the database
                var questionEntity = DataBaseLocator.Database.Questions.FirstOrDefault(q => q.Id == question.Id);
                if (questionEntity != null)
                {
                    DataBaseLocator.Database.Questions.Remove(questionEntity);
                }
            }
            DataBaseLocator.Database.SaveChanges();
        }

        private void deleteSelectedQuiz()
        {
            var selectedQuizzes = QuizzesList.Where(q => q.IsSelected).ToList();

            foreach (var quiz in selectedQuizzes)
            {
                QuizzesList.Remove(quiz);

                // Remove all questions associated with the quiz from the database
                var quizEntity = DataBaseLocator.Database.Quizzes.FirstOrDefault(q => q.Id == quiz.Id);
                if (quizEntity != null)
                {
                    foreach (var question in quizEntity.Questions.ToList())
                    {
                        quizEntity.Questions.Remove(question);
                        DataBaseLocator.Database.Questions.Remove(question);
                    }

                    DataBaseLocator.Database.Quizzes.Remove(quizEntity);
                }
            }
            DataBaseLocator.Database.SaveChanges();
        }



    }
}
