using AviaSea.DateBase;
using AviaSea.Models;
using AviaSea.VIew;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaSea.ViewModel
{
    public class ViewModelAllPosts
    {
        public ObservableCollection<Models.Posts> Posts { get; } = new ObservableCollection<Models.Posts>();
        public ViewModelAllPosts()
        {
            using(var context = new AviaSeaContext())
            {
                var items = context.AllPosts;
                foreach(var item in items)
                {
                    Models.Posts posts = new Models.Posts()
                    {
                        PostId = item.IdPosts,
                        ImagePost = item.ImagePosts,
                       ImageUser = item.Img,
                       FamilyUser = item.Family,
                       NameUser = item.Name,
                       
                        info = item.Info,
                        kolvo_see = (int)item.KolvoSee,
                    };
                    Posts.Add(posts);
                }          
            }
        }
    }
}
