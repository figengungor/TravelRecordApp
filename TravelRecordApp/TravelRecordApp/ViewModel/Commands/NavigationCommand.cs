using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel.Commands
{
    public class NavigationCommand : ICommand

    {
        private HomeVM HomeViewModel;
        public event EventHandler CanExecuteChanged;

        public NavigationCommand(HomeVM homeVM)
        {
            HomeViewModel = homeVM;
        }

        public bool CanExecute(object parameter)
        {
            return true; // we don't need to do any evaluation while navigating
        }

        public void Execute(object parameter)
        {
            HomeViewModel.Navigate();
        }
    }
}
