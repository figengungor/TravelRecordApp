using Microsoft.WindowsAzure.MobileServices;
using System;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace TravelRecordApp
{
	public partial class App : Application
	{
        public static string DatabaseLocation = string.Empty;
        public static MobileServiceClient MobileService =
            new MobileServiceClient(
            "https://travelrecordappfig.azurewebsites.net"
        );

        public static User user = new User(); 

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
