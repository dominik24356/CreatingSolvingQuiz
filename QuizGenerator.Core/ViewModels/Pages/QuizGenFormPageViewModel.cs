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

            //DatabaseLocator.Database.WorkTasks.Add(new WorkTask
            //{
            //    Id = newTask.Id,
            //    Title = newTask.Title,
            //    Description = newTask.Description,
            //    CreatedDate = newTask.CreatedDate
            //});

            //DatabaseLocator.Database.SaveChanges();

            //NewWorkTaskTitle = String.Empty;
            //NewWorkTaskDescription = String.Empty;
        }

        private void addNewQuiz()
        {
            var newQuiz = new QuizViewModel
            {
                Questions = QuestionsList.ToList(),
                QuizTitle = QuizName
            };
            QuizzesList.Add(newQuiz);

            //DatabaseLocator.Database.WorkTasks.Add(new WorkTask
            //{
            //    Id = newTask.Id,
            //    Title = newTask.Title,
            //    Description = newTask.Description,
            //    CreatedDate = newTask.CreatedDate
            //});

            //DatabaseLocator.Database.SaveChanges();

            //NewWorkTaskTitle = String.Empty;
            //NewWorkTaskDescription = String.Empty;
        }


    }
}
