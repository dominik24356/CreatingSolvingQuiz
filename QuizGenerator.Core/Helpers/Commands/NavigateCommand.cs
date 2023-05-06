using QuizGenerator.Core.Commands;
using QuizGenerator.Core.Stores;
using QuizGenerator.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator.Core.Helpers.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : BaseViewModel
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
