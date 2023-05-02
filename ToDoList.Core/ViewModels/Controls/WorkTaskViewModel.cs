using System;
using ToDoList.Core.ViewModels.Base;

namespace ToDoList.Core.ViewModels.Controls
{
    public class WorkTaskViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
