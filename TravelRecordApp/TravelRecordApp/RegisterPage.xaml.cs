using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        User user;

		public RegisterPage ()
		{
			InitializeComponent ();
            user = new User();
            containerStackLayout.BindingContext = user;
		}

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {            
                User.Register(user);
            }
            else {
                await DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }
    }
}