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
using ToDoList.Database.Entities;

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

            foreach (var task in DatabaseLocator.Database.WorkTasks.ToList())
            {
                WorkTaskList.Add(new WorkTaskViewModel // mapowanie, mozna by to wrzcic gdzies do innej klasy
                {
                    Title = task.Title,
                    Description = task.Description,
                    CreatedDate = task.CreatedDate
                });
            }

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

            DatabaseLocator.Database.WorkTasks.Add(new WorkTask
            {
              Id = newTask.Id,
              Title = newTask.Title  ,
              Description = newTask.Description,
              CreatedDate = newTask.CreatedDate
            });

            DatabaseLocator.Database.SaveChanges();

            NewWorkTaskTitle = String.Empty;
            NewWorkTaskDescription = String.Empty;

        }


        private void DeleteSelectedTasks()
        {

            var selectedTasks = WorkTaskList.Where(x => x.IsSelected).ToList();

            foreach (var task in selectedTasks)
            {
                WorkTaskList.Remove(task);

                var foundEntity = DatabaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == task.Id);

                if(foundEntity != null)
                {
                    DatabaseLocator.Database.Remove(foundEntity);
                }
            }

            DatabaseLocator.Database.SaveChanges();

        }

    }
}
