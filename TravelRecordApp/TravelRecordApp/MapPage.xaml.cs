using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
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
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();

		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // IsShowingUser="True" set after getting permission
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need permission", "We will have to access your location", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    locationsMap.IsShowingUser = true;

                    var locator = CrossGeolocator.Current;
                    locator.PositionChanged += Locator_PositionChanged;
              
                    await locator.StartListeningAsync(TimeSpan.Zero, 100);
                   
                    var position = await locator.GetPositionAsync();
                               
                    var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
                    locationsMap.MoveToRegion(span);
                }
                else
                {                 
                    await DisplayAlert("No permission", "You didn't grant permission to access your location, we cannot proceed.", "Ok");
                }

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    var posts = conn.Table<Post>().ToList();

                    DisplayInMap(posts);
                }


            }
            catch (Exception e)
            {

            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;

            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts) {

                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);

                }
                catch(NullReferenceException nre){}
                catch (Exception ex) { }

            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);
        }
    }
}