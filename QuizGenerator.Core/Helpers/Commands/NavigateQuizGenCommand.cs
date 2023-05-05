using QuizGenerator.Core.Stores;
using QuizGenerator.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator.Core.Commands
{
    public class NavigateQuizGenCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;

        public NavigateQuizGenCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new QuizGenFormPageViewModel();
        }
    }
}
