using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuizGen.Commands;

namespace QuizGen.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {

        private string _screenVal;

        public MainViewModel()
        {
            ScreenVal = "0";
            AddNumberCommand = new RelayCommand(AddNumber);
        }

        private void AddNumber(object obj)
        {
            MessageBox.Show(obj as string);
        }

        public ICommand AddNumberCommand { get; set; }

        public string ScreenVal
        {
            get { return _screenVal; }
            set
            {
                _screenVal = value;
                onPropertyChanged(); // ta metoda to jest zeby po zmianach w viewmodelu odswiezyly sie dane w widoku
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // w zaleznosci gdzie ta metoda zostanie wywolana to taki zostanie przekazany do niej parametr dzieki callerowi
        private void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
