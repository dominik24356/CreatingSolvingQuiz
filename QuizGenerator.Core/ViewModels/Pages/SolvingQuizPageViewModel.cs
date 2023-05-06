using Microsoft.EntityFrameworkCore;
using QuizGenerator.Core.Helpers;
using QuizGenerator.Core.Helpers.Commands;
using QuizGenerator.Core.ViewModels.Base;
using QuizGenerator.Core.ViewModels.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;






namespace QuizGenerator.Core.ViewModels
{
    public class SolvingQuizPageViewModel : BaseViewModel
    {

        public ObservableCollection<QuizViewModel> QuizzesList { get; set; } = new ObservableCollection<QuizViewModel>();

        public ObservableCollection<QuestionViewModel> QuestionsList { get; set; } = new ObservableCollection<QuestionViewModel>();
        public ObservableCollection<QuestionViewModel> QuestionsListToShow { get; set; } = new ObservableCollection<QuestionViewModel>();

        public string QuizName { get; set; } = "Nie wybrano";

        public ICommand ChooseQuizToSolveCommand { get; set; }

        public ICommand StartQuizCommand { get; set; }

        public ICommand ResetQuizCommand { get; set; }

        public SolvingQuizPageViewModel()
        {
            //odczyt quizow z pytaniami
            foreach (var quiz in DataBaseLocator.Database.Quizzes.Include(q => q.Questions).ToList())
            {
                // przypisanie komend
                ChooseQuizToSolveCommand = new RelayCommand(ChooseQuizToSolve);
                StartQuizCommand = new RelayCommand(StartQuiz);
                ResetQuizCommand = new RelayCommand(ResetQuiz);


                QuizzesList.Add(new QuizViewModel
                {
                    Id = quiz.Id,
                    QuizTitle = AesEncryptionHelper.DecryptString(quiz.Name),
                    Questions = QuestionViewModelMapper.mapToViewModelList(quiz.Questions)
                }
                );
            }
        }



        public void ChooseQuizToSolve()
        {
            
            var selectedQuiz = QuizzesList.FirstOrDefault(q => q.IsSelected);
            if (selectedQuiz == null)
                return;

            QuestionsList.Clear();

            foreach (var question in selectedQuiz.Questions)
            {
                var questionViewModel = new QuestionSolvingViewModel
                {
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectOption = question.CorrectOption,
                    SelectedOption = null
                };
                QuizName = selectedQuiz.QuizTitle;

                QuestionsList.Add(questionViewModel);
            }

                
            

        }

        public void StartQuiz() 
        {
            // kopiowanie wczytanego quizu do quizu pokazywanego
            QuestionsListToShow = new ObservableCollection<QuestionViewModel>();
            foreach (QuestionViewModel question in QuestionsList)
            {
                QuestionsListToShow.Add(question);
            }
        }

        public void ResetQuiz()
        {
            // resetowanie quizu wczytanego i kopiowanego
            QuestionsList.Clear();
            QuestionsListToShow = null;
            QuizName = "Nie wybrano";

        }




    }
}
