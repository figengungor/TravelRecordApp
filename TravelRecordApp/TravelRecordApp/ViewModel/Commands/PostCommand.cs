using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class PostCommand : ICommand
    {

        private NewTravelVM NewTravelViewModel;

        public PostCommand(NewTravelVM newTravelViewModel)
        {
            NewTravelViewModel = newTravelViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var post = (Post)parameter;

            if(post==null)
                return false;

            if (string.IsNullOrEmpty(post.Experience) || post.Venue == null) {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            NewTravelViewModel.PublishPost();
        }
    }
}
