using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
        HistoryVM historyViewModel;

		public HistoryPage ()
		{
			InitializeComponent ();
            historyViewModel = new HistoryVM();
            BindingContext = historyViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            historyViewModel.UpdatePosts();
              
        }

    }
}