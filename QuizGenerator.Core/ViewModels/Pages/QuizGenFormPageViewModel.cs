using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
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
        public ICommand ConfirmEditQuestionCommand { get; set; }
        public ICommand FillFormQuestionCommand { get; set; }
        public ICommand FillFormQuizCommand { get; set; }
        public ICommand ConfirmEditQuizCommand { get; set; }


        public QuizGenFormPageViewModel()
        {
            AddNewQuestionCommand = new RelayCommand(AddNewQuestion);
            AddNewQuizCommand = new RelayCommand(AddNewQuiz);
            DeleteSelectedQuestionsCommand = new RelayCommand(deleteSelectedQuestions);
            DeleteSelectedQuizCommand = new RelayCommand(deleteSelectedQuiz);
            ConfirmEditQuestionCommand = new RelayCommand(editSelectedQuestion);
            FillFormQuestionCommand = new RelayCommand(fillTheFieldsFromQuestion);
            FillFormQuizCommand = new RelayCommand(fillTheFieldsFromQuiz);
            ConfirmEditQuizCommand = new RelayCommand(EditSelectedQuiz);


            //odczyt quizow z pytaniami
            foreach(var quiz in DataBaseLocator.Database.Quizzes.Include(q => q.Questions).ToList())
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
                Name = AesEncryptionHelper.EncryptString(QuizName),
                Questions = new List<Question>()
            };

            foreach (var question in QuestionsList)
            {
                quizEntity.Questions.Add(new Question
                {
                    QuestionText = AesEncryptionHelper.EncryptString(question.QuestionText),
                    OptionA = AesEncryptionHelper.EncryptString(question.OptionA),
                    OptionB = AesEncryptionHelper.EncryptString(question.OptionB),
                    OptionC = AesEncryptionHelper.EncryptString(question.OptionC),
                    OptionD = AesEncryptionHelper.EncryptString(question.OptionD),
                    CorrectOption = AesEncryptionHelper.EncryptString(question.CorrectOption)
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

        private void fillTheFieldsFromQuestion()
        {
            var selectedQuestion = QuestionsList.FirstOrDefault(q => q.IsSelected);
            if (selectedQuestion != null)
            {
                QuestionText = selectedQuestion.QuestionText;
                AnswerA = selectedQuestion.OptionA;
                AnswerB = selectedQuestion.OptionB;
                AnswerC = selectedQuestion.OptionC;
                AnswerD = selectedQuestion.OptionD;
                ProperAnswer = selectedQuestion.CorrectOption;
            }
        }


        private void editSelectedQuestion()
        {
            var selectedQuestion = QuestionsList.FirstOrDefault(q => q.IsSelected);

            if (selectedQuestion == null)
                return;

            // update the selected question view model with new values
            selectedQuestion.QuestionText = this.QuestionText;
            selectedQuestion.OptionA = AnswerA;
            selectedQuestion.OptionB = AnswerB;
            selectedQuestion.OptionC = AnswerC;
            selectedQuestion.OptionD = AnswerD;
            selectedQuestion.CorrectOption = ProperAnswer;       
   

            // reset the input fields
            QuestionText = string.Empty;
            AnswerA = string.Empty;
            AnswerB = string.Empty;
            AnswerC = string.Empty;
            AnswerD = string.Empty;
            ProperAnswer = string.Empty;
        }

        private void fillTheFieldsFromQuiz()
        {
            var selectedQuiz = QuizzesList.FirstOrDefault(q => q.IsSelected);
            if (selectedQuiz == null)
                return;

            QuestionsList.Clear();

            foreach (var question in selectedQuiz.Questions)
            {
                var questionViewModel = new QuestionViewModel
                {
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectOption = question.CorrectOption
                };

                QuestionsList.Add(questionViewModel);
            }

            // Set quiz name in the input field
            QuizName = selectedQuiz.QuizTitle;
        }



        private void EditSelectedQuiz()
        {
            // Find the selected quiz in the QuizzesList
            var selectedQuiz = QuizzesList.FirstOrDefault(q => q.IsSelected);
            if (selectedQuiz == null) return;

            // Update the selected quiz with the values from the form
            selectedQuiz.QuizTitle = QuizName;
            selectedQuiz.Questions = QuestionsList.ToList();

            // Update the corresponding quiz in the database
            var quizEntity = DataBaseLocator.Database.Quizzes.Find(selectedQuiz.Id);
            if (quizEntity == null) return;

            quizEntity.Name = AesEncryptionHelper.EncryptString(QuizName);
            quizEntity.Questions.Clear();

            foreach (var question in QuestionsList)
            {
                quizEntity.Questions.Add(new Question
                {
                    QuestionText = AesEncryptionHelper.EncryptString(question.QuestionText),
                    OptionA = AesEncryptionHelper.EncryptString(question.OptionA),
                    OptionB = AesEncryptionHelper.EncryptString(question.OptionB),
                    OptionC = AesEncryptionHelper.EncryptString(question.OptionC),
                    OptionD = AesEncryptionHelper.EncryptString(question.OptionD),
                    CorrectOption = AesEncryptionHelper.EncryptString(question.CorrectOption)
                });
            }

            DataBaseLocator.Database.SaveChanges();

            // Clear the form
            QuizName = string.Empty;
            QuestionsList.Clear();
        }


    }
}
