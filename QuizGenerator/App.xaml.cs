using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using QuizGenerator.Core;
using QuizGenerator.Core.Stores;
using QuizGenerator.Core.ViewModels;
using QuizGenerator.Database;
using System.Threading;

namespace QuizGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            var database = new QuizGeneratorDbContext();

            database.Database.EnsureCreated();

            DataBaseLocator.Database = database;

            base.OnStartup(e);



            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new MenuPageViewModel(navigationStore);

            // utworzenie Page MainWindow i przypisanie do niego viewModel
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            
            MainWindow.Show();
            

            

            

        }


    }
}
