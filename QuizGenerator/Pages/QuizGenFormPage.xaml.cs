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
using QuizGenerator.Core.ViewModels.Pages;

namespace QuizGenerator
{
    /// <summary>
    /// Interaction logic for QuizGenFormPage.xaml
    /// </summary>
    public partial class QuizGenFormPage : Page
    {
        public QuizGenFormPage()
        {
            InitializeComponent();
            DataContext = new QuizGenFormPageViewModel();  // tutaj podpięty jest viewmodel pod naszą stronę

        }
    }
}
