using QuizGenerator.Core.Commands;
using QuizGenerator.Core.Helpers;
using QuizGenerator.Core.Helpers.Commands;
using QuizGenerator.Core.Stores;
using QuizGenerator.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizGenerator.Core.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public ICommand startGeneratingCommand { get; set; }
        public ICommand startSolvingCommand { get; set; }

        

        public MenuPageViewModel(NavigationStore navigationStore) 
        {
            startGeneratingCommand = new NavigateQuizGenCommand(navigationStore);
            startSolvingCommand = new NavigateQuizSolvCommand(navigationStore);
            
        }

        
    }
}
