using AviaSea.DateBase;
using AviaSea.Infostraction.Commands;
using AviaSea.Models;
using AviaSea.VIew;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AviaSea.ViewModel
{
    public class ViewModelAllPosts
    {
        public ObservableCollection<Models.Posts> Posts { get; } = new ObservableCollection<Models.Posts>();
        public ViewModelAllPosts()
        {
            OpenDetailInfoPosts = new LambdaCommand(CanOpenDetailInfoPosts, OnOpenDetailInfoPosts);
            using (var context = new AviaSeaContext())
            {
                var items = context.AllPosts.OrderByDescending(s => s.IdPosts);
                foreach (var item in items)
                {
                    Models.Posts posts = new Models.Posts()
                    {
                        PostId = item.IdPosts,
                        ImagePost = item.ImagePosts,
                       //ImageUser = item.
                        info = item.Info,
                        kolvo_see = (int)item.KolvoSee,
                    };
                    Posts.Add(posts);
                    //Posts.OrderBy(s => s.PostId);
                }
            }
        }

        public delegate void ShowWindow();
        public event ShowWindow? OpenWindow;

        public ICommand OpenDetailInfoPosts { get; set; }
        private bool OnOpenDetailInfoPosts(object obj) => true;
        private void CanOpenDetailInfoPosts(object obj)
        {
            OpenWindow?.Invoke();
        }
        public static Models.Posts selectedPosts;
        public Models.Posts SelectedPosts
        {
            get { return selectedPosts; }
            set
            {
                selectedPosts = value;
                OpenWindow?.Invoke();
            }
        }
    }
}
