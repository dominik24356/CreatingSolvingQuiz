﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizGenerator.Core.Helpers
{
    class RelayCommand : ICommand
    {

        private Action mAction;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true; //zawsze da sie odpalic dana funkcje
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}