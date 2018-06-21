using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterVM RegisterViewModel;

        public RegisterCommand(RegisterVM registerViewModel)
        {
            RegisterViewModel = registerViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            User user = (User)parameter;

            if (user == null)
                return false;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.ConfirmPassword)) 
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            RegisterViewModel.Register();
        }
    }
}
