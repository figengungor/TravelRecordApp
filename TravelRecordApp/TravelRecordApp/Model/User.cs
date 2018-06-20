using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public static async Task<bool> Login(string email, string password) {

            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                return false;
            }
            else
            {
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
                if (user != null)
                {
                    App.user = user;

                    if (user.Password == password)
                    {
                        //await Navigation.PushAsync(new HomePage());
                        return true;
                    }
                    else
                    {
                        //await DisplayAlert("Error", "Email or password are incorrect", "Ok");
                        return false;
                    }
                }
                else
                {
                    //await DisplayAlert("Error", "There was an error logging you in", "Ok");
                    return false;
                }

            }
        }

        public static async void Register(User user)
        {
            await App.MobileService.GetTable<User>().InsertAsync(user);
        }

    }
}
