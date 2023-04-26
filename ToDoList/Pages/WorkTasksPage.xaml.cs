using System.Windows.Controls;
using ToDoList.Core.ViewModels.Pages;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for WorkTasksPage.xaml
    /// </summary>
    public partial class WorkTasksPage : Page
    {
        public WorkTasksPage()
        {
            InitializeComponent();

            DataContext = new WorkTasksPageViewModel();  // tutaj podpięty jest viewmodel pod naszą stronę
        }
    }
}
