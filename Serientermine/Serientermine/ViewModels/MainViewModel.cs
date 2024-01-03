using Hmd.Core.UI.Commands;
using System;
using System.Windows.Input;

namespace Serientermine.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private DateTime? _start;

        public MainViewModel()
        {
            CalcCommand = new DelegateCommand(() => Start = DateTime.Now);    
        }

        public ICommand CalcCommand { get; }

        public DateTime? Start 
        { 
            get { return _start; } 
            set 
            {
                if (_start == value)
                    return;

                _start = value;
                OnPropertyChanged(nameof(Start));
            }
        }
       

        

        //private void SubmitButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Try to parse the input as a number
        //    if (double.TryParse(numberTextBox.Text, out double result))
        //    {
        //        MessageBox.Show($"You entered a valid number: {result}");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid input. Please enter a valid number.");
        //    }
        //}
    }
}
