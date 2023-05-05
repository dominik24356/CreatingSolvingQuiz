using QuizGenerator.Core.Commands;
using QuizGenerator.Core.Stores;
using QuizGenerator.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator.Core.Helpers.Commands
{
    public class NavigateQuizSolvCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;

        public NavigateQuizSolvCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new SolvingQuizPageViewModel();
        }
    }
}
