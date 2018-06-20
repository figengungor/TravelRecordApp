using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]  //Sqlite Attributes
        public string Id { get; set; }

        [MaxLength(250)] //if Experince is longer than 250 chars, it will throw an error and it will not be saved to db
        public string Experience { get; set; }

        public string VenueName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }

        public string UserId { get; set; }

        public static async void Insert(Post post)
        {
            await App.MobileService.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<List<Post>> Read()
        {
            return await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();
        }

        public static Dictionary<string, int> PostCategories(List<Post> posts) {

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

            return categoriesCount;

        }

    }
}
