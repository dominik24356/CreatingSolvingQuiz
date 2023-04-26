using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.ViewModels.Base;
using ToDoList.Core.ViewModels.Controls;

namespace ToDoList.Core.ViewModels.Pages
{
    public class WorkTasksPageViewModel : BaseViewModel
    {

        public ObservableCollection<WorkTaskViewModel> WorkTaskList { get; set; } = new ObservableCollection<WorkTaskViewModel>();

        public string NewWorkTaskTitle { get; set; }

        public string NewWorkTaskDescription { get; set; }

        public ICommand AddNewTaskCommand { get; set; }

        public ICommand DeleteSelectedTasksCommand { get; set; }

        public WorkTasksPageViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        }

        private void AddNewTask()
        {
            var newTask = new WorkTaskViewModel
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                CreatedDate = DateTime.Now
            };
            WorkTaskList.Add(newTask);

            NewWorkTaskTitle = String.Empty;
            NewWorkTaskDescription = String.Empty;

            OnPropertyChanged(nameof(NewWorkTaskTitle)); // to jest m.in odpowiedzialne za to zeby dzialaly referencje w dwie strony (Mode = "TwoWay")
            OnPropertyChanged(nameof(NewWorkTaskDescription));

        }


        private void DeleteSelectedTasks()
        {

            var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();

            foreach (var task in selectedTasks)
            {
                WorkTaskList.Remove(task);
            }

        }

    }
}
