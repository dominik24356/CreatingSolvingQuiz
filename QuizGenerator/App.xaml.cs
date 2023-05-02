using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using QuizGenerator.Core;
using QuizGenerator.Database;

namespace QuizGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var database = new QuizGeneratorDbContext();

            database.Database.EnsureCreated();

            DataBaseLocator.Database = database;

        }


    }
}
