using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterNavigationCommand : ICommand
    {
        MainVM MainViewModel;

        public RegisterNavigationCommand(MainVM mainViewModel) {
            MainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainViewModel.Navigate();
        }
    }
}
