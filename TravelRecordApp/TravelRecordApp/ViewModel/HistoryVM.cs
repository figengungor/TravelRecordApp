using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel
{
    public class HistoryVM
    {
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
        }

        public async void UpdatePosts() {

            var posts = await Post.Read();
            if (posts != null) { //at first start OnAppearing is called twice, to avoid posts to be added twice we do this check
                foreach(Post post in posts) 
                {
                    Posts.Add(post);
                }
            }

        }


    }
}
