using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        PermissionStatus status;
        bool hasRequestedPermission = false;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // IsShowingUser="True" set after getting permission
            Debug.WriteLine("OnAppearing is called!!!!!!!!!!!!");
            try
            {
                var newStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (newStatus!=status || !hasRequestedPermission)//Add these checks to avoid onAppearing loop
                {
                    status = newStatus;

                    if(status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        {
                            await DisplayAlert("Need permission", "We will have to access your location", "Ok");
                        }
                        //This triggers the onAppearing again when permission denied without condition checks
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                        hasRequestedPermission = true;                     
                        status = results[Permission.Location];
                    }                  
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

                    var posts = await Post.Read();
                    DisplayInMap(posts);
                }
                else
                {
                    //When MapPage is appeared first time, this is called twice(onAppearing called twice) but then swiping to MapPage triggers this only once                 
                    await DisplayAlert("No permission", "You didn't grant permission to access your location, we cannot proceed.", "Ok");
                }

            }
            catch (Exception e)
            {
                //await DisplayAlert("No permission", "You didn't grant permission to access your location, we cannot proceed.", "Ok");
                Debug.WriteLine($"Exception: {e.Message}");
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