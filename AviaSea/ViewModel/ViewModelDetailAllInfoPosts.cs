using AviaSea.DateBase;
using AviaSea.Infostraction.Commands;
using AviaSea.Models;
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
    public class ViewModelDetailAllInfoPosts
    {
        public delegate void ShowWindow();
        public event ShowWindow? CloseDetailInfo;
        public ViewModelDetailAllInfoPosts()
        {
            using (var context = new AviaSeaContext())
            {
                var item = context.AllPosts.SingleOrDefault(s => s.IdPosts == ViewModelAllPosts.selectedPosts.PostId);
                if (item != null)
                {
                    Models.Posts posts = new Models.Posts()
                    {
                        PostId = item.IdPosts,
                        ImagePost = item.ImagePosts,
                        ImageUser = item.Img,
                        FamilyUser = item.Family,
                        NameUser = item.Name,
                        date = Convert.ToDateTime(item.Date),
                        info = item.Info,
                        kolvo_see = (int)item.KolvoSee,
                    };
                    Posts.Add(posts);              
                }
            }
            ClosePage = new LambdaCommand(CanClosePage, OnClosePage);
            SelectedPost = ViewModelAllPosts.selectedPosts;
            MessageBox.Show(SelectedPost.info);
        }
        public ICommand ClosePage { get; set; }
        private bool OnClosePage(object obj) => true;
        private void CanClosePage(object obj)
        {
            CloseDetailInfo?.Invoke();
        }
        public Posts SelectedPost { get; set; }

        public ObservableCollection<Models.Posts> Posts { get; set; } = new ObservableCollection<Models.Posts>();
    }
}
