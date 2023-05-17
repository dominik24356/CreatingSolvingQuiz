using Microsoft.EntityFrameworkCore;
using QuizGenerator.Core.Helpers;
using QuizGenerator.Core.Helpers.Commands;
using QuizGenerator.Core.ViewModels.Base;
using QuizGenerator.Core.ViewModels.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;






namespace QuizGenerator.Core.ViewModels
{
    public class SolvingQuizPageViewModel : BaseViewModel
    {

        public ObservableCollection<QuizViewModel> QuizzesList { get; set; } = new ObservableCollection<QuizViewModel>();

        public ObservableCollection<QuestionSolvingViewModel> QuestionsList { get; set; } = new ObservableCollection<QuestionSolvingViewModel>();
        public ObservableCollection<QuestionSolvingViewModel> QuestionsListToShow { get; set; } = new ObservableCollection<QuestionSolvingViewModel>();

        public string QuizName { get; set; } = "Nie wybrano";

        public QuizState quizStatus = QuizState.QuizNotStarted;

        public enum QuizState
        {
            QuizNotStarted,
            QuizStarted ,
            QuizEnded
        }

        public bool isLoaded = false;

        public String isQuizStartedName { get; set; } = "Quiz nie został jeszcze rozpoczęty !";

        public int points { get; set; } = 0;

        public int maxPoints { get; set; }

        public ICommand ChooseQuizToSolveCommand { get; set; }

        public ICommand StartQuizCommand { get; set; }

        public ICommand ResetQuizCommand { get; set; }

        public ICommand EndQuizCommand { get; set; }

        

        public SolvingQuizPageViewModel()
        {
            // przypisanie komend
            ChooseQuizToSolveCommand = new RelayCommand(ChooseQuizToSolve);
            StartQuizCommand = new RelayCommand(StartQuiz);
            ResetQuizCommand = new RelayCommand(ResetQuiz);
            EndQuizCommand = new RelayCommand(EndQuiz);
            


            //odczyt quizow z pytaniami
            foreach (var quiz in DataBaseLocator.Database.Quizzes.Include(q => q.Questions).ToList())
            {

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
            ResetQuiz();
   
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

            maxPoints = QuestionsList.Count();
                
            isLoaded = true;

        }

        public void StartQuiz() 
        {
            points = 0;


            if (!isLoaded || quizStatus == QuizState.QuizStarted)
            {
                return;
            }
            else
            {
                quizStatus = QuizState.QuizStarted;
                setStartStopName(quizStatus);
            }
 
            // kopiowanie wczytanego quizu do quizu pokazywanego
            QuestionsListToShow = new ObservableCollection<QuestionSolvingViewModel>();
            foreach (QuestionSolvingViewModel question in QuestionsList)
            {
                QuestionsListToShow.Add(question);
            }
        }

        public void ResetQuiz()
        {
            maxPoints = 0;

            quizStatus = QuizState.QuizNotStarted;
            setStartStopName(QuizState.QuizNotStarted);

            points = 0;
            isLoaded = false;
            // resetowanie quizu wczytanego i kopiowanego
            QuestionsList.Clear();
            QuestionsListToShow = null;

            QuizName = "Nie wybrano";
        }

        private void EndQuiz()
        {

            if (quizStatus != QuizState.QuizStarted || QuestionsListToShow.Any(question => question.SelectedOption == null))
            {
                return;
            }

            


            setStartStopName(QuizState.QuizEnded);

            foreach (var question in QuestionsListToShow)
            {

                if (question.CorrectOption.ToUpper().Equals(question.SelectedOption)){
                    points++;
                }
            }

            quizStatus = QuizState.QuizEnded;
        }


        public void setStartStopName(QuizState quizStatus)
        {
            if (quizStatus == QuizState.QuizStarted)
            {
                isQuizStartedName = "Quiz trwa !";
            }
            else if(quizStatus == QuizState.QuizNotStarted)
            {
                isQuizStartedName = "Quiz nie został jeszcze rozpoczęty !";
            }
            else
            {
                isQuizStartedName = "Quiz został zakończony !";
            }
        }

        


    }
}
