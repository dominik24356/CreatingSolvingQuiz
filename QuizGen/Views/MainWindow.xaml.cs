using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using QuizGen.Database;
using QuizGen.ViewModels;

namespace QuizGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MyDbContext dbContext = new MyDbContext();
        public MainWindow()
        {
            var quiz = dbContext.quizzes.Include(q => q.Questions).FirstOrDefault(q => q.Id == 1);
            var questions = quiz.Questions;

            foreach (var question in questions)
            {
                Console.WriteLine($"- {question.OptionA}, {question.OptionB}, {question.OptionC}, {question.OptionD}");
            }

            DataContext = new MainViewModel();
            // tutaj wskazujemy ze dla okna MainWindow to jest viewModel z logiką i tam będziemy wiązali dane
            InitializeComponent();
        }
    }
}
