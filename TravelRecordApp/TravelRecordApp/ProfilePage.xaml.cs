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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{

            //conn.CreateTable<Post>();

            //var postTable = conn.Table<Post>();

            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

            //var posts = postTable.ToList(); //get TableQuery as list with Linq
                    
                    //Get list and query from this list similar to sqlite with Linq
                    var categories = (from p in posts
                                      orderby p.CategoryId
                                      select p.CategoryName).Distinct().ToList();

                    Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

                    foreach (var category in categories)
                    {
                        var count = (from post in posts
                                     where post.CategoryName == category
                                     select post).ToList().Count;

                        categoriesCount.Add(category, count);
                    }

                    categoriesListView.ItemsSource = categoriesCount;

                    postCountLabel.Text = posts.Count.ToString();

                //}

        }
    }
}