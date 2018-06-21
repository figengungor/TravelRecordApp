using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {

        public RegisterCommand RegisterCommand { get; set; }

        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new User
                {
                    Email = email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User
                {
                    Email = this.Email,
                    Password = password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }



        public RegisterVM()
        {
            RegisterCommand = new RegisterCommand(this);
            User = new User();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void Register()
        {
            if (User.Password == User.ConfirmPassword)
            {
                User.Register(User);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }
    }
}
