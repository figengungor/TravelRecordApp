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
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        PermissionStatus status;
        bool hasRequestedPermission = false;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("OnAppearing is called from NewTravelPage!!!");

            try
            {
                var newStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (newStatus != status || !hasRequestedPermission)//Add these checks to avoid onAppearing loop
                {
                    status = newStatus;

                    if (status != PermissionStatus.Granted)
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
                    var locator = CrossGeolocator.Current;

                    var position = await locator.GetPositionAsync();

                    var venues = await Venue.GetVenues(position.Latitude, position.Longitude);

                    venueListView.ItemsSource = venues;
                }
                else
                {         
                    //TODO: check that unpredicted onAppearing later, it happends once or twice
                    Debug.WriteLine("Permission denied NewTravelPage!!!");
                    await DisplayAlert("No permission", "You didn't grant permission to access your location, we cannot proceed.", "Ok");
                }

            }
            catch (Exception e)
            {             
                Debug.WriteLine($"Exception: {e.Message}");
                //await DisplayAlert("No permission", "You didn't grant permission to access your location, we cannot proceed.", "Ok");
            }

          
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {

                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();


                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name,
                    UserId = App.user.Id,
                };

                /*using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    int row = conn.Insert(post);

                    if (row > 0)
                    {
                        DisplayAlert("Success", "Experience is added successfuly", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Failure", "Experience couldn't be added!", "Ok");
                    }

                }*/

                Post.Insert(post);
                await DisplayAlert("Success", "Experience is added successfuly", "Ok");
            }
            catch (NullReferenceException nre)
            {
                await DisplayAlert("Failure", "Experience couldn't be added!", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", "Experience couldn't be added!", "Ok");
            }


        }
    }
}