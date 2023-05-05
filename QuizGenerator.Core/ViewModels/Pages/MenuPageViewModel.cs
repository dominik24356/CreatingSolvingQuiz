using QuizGenerator.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizGenerator.Core.ViewModels.Pages
{
    public class MenuPageViewModel
    {
        public ICommand startGeneratingCommand { get; set; }
        public ICommand startSolvingCommand { get; set; }

        public MenuPageViewModel()
        {
            startGeneratingCommand = new RelayCommand(startGenerating);
            startSolvingCommand = new RelayCommand(startSolving);
        }


        public void startGenerating() 
        {
            
        }

        public void startSolving()
        { 
        }
    }
}
