using AviaSea.DateBase;
using AviaSea.Infostraction.Commands;
using AviaSea.Models;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace AviaSea.ViewModel
{
    public class VIewModelUserProfil : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string names)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(names));
            }
        }
        public VIewModelUserProfil()
        {
            CommandSelectedAvatar = new LambdaCommand(CanCommandSelectedAvatar, OnCommandSelectedAvatar);
            EditingUserProfil = new LambdaCommand(CanEditingUserProfil, OnEditingUserProfil);
            AttachFile = new LambdaCommand(CanAttachFile, OnAttachFile);
            PublishPost = new LambdaCommand(CanPublishPost, OnPublishPost);
            DeletedPost = new LambdaCommand(CanDeletedPost, OnDeletedPost);
            using (var context = new AviaSeaContext())
            {
                var item = context.Users.SingleOrDefault(s => s.IdUser == ViewModelAutorization.id_user);
                FamilyUser = item.Family;
                NameUser = item.Name;
            }
            SeePostsUsers();
        }
        private string familyUser;
        public string FamilyUser { get; set; }
        public string NameUser { get; set; }

        private void SeePostsUsers()
        {
            using (var context = new AviaSeaContext())
            {
                UserPosts.Clear();
                var items = context.AllPosts.Where(s => s.IdUser == ViewModelAutorization.id_user).OrderByDescending(s=>s.IdPosts); // Отображение постов пользователя
                foreach (var item in items)
                {
                    Models.Posts post = new Models.Posts
                    {
                        FamilyUser = item.Family,
                        NameUser = item.Name,
                        info = item.Info,
                        kolvo_see = (int)item.KolvoSee,
                        PostId = item.IdPosts,
                        ImagePost = item.ImagePosts,
                        ImageUser = item.Img
                    };
                    UserPosts.Add(post);
                }
            }
        }

        public ICommand CommandSelectedAvatar { get; } // Команда выбора аватарки
        private bool OnCommandSelectedAvatar(object obj) => true;
        private void CanCommandSelectedAvatar(object obj)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName);
                using (var context = new AviaSeaContext())
                {
                    var item = context.Users.SingleOrDefault(s => s.IdUser == ViewModelAutorization.id_user);
                    item.Img = image_bytes;
                    context.Users.Update(item);
                    context.SaveChanges();
                }
            }
            catch
            {

            }
        }
        public BitmapImage UpdateImage // Обновление аватарки пользователя
        {
            get
                 {
                 using (var context = new AviaSeaContext())
                 {
                     var item = context.Users.SingleOrDefault(s => s.IdUser == ViewModelAutorization.id_user);
                     if (item.Img != null)
                     {
                         MemoryStream memorystream = new MemoryStream();
                         memorystream.Write(item.Img, 0, (int)item.Img.Length);
                         memorystream.Seek(0, SeekOrigin.Begin);
                         BitmapImage imgsource = new BitmapImage();
                         imgsource.BeginInit();
                         imgsource.StreamSource = memorystream;
                         imgsource.EndInit();
                         return imgsource; // реальный Image
                     }
                     return null;
                 }
            }
        }
        public ObservableCollection<Models.Posts> UserPosts { get; } = new ObservableCollection<Models.Posts>();


        public ICommand EditingUserProfil { get; set; }
        private bool OnEditingUserProfil(object obj) => true;

        private void CanEditingUserProfil(object obj)
        {

        }

        public ICommand PublishPost { get; set; }
        private bool OnPublishPost(object obj)
        {
            if (ContentInPost != null && ContentInPost != "")
            {
                return true;
            }
            return false;
        }

        private void CanPublishPost(object obj)
        {
            using (var context = new AviaSeaContext())
            {
                DateTime dateTime = DateTime.Now;
                var newitem = new DateBase.Post()
                {
                    IdUser = ViewModelAutorization.id_user,
                    Info = contentInPost,
                    ImagePosts = imagePost,
                    KolvoSee = 0,
                    Date = dateTime,
                };
                context.Posts.Add(newitem);
                context.SaveChanges();
                SeePostsUsers();
            }
        }

        private byte[] imagePost;

        public byte[] ImagePost
        {
            get
            {
                return imagePost;
            }
            set
            {
                if (value != null)
                {
                    MemoryStream memorystream = new MemoryStream();
                    memorystream.Write(value, 0, (int)value.Length);
                    memorystream.Seek(0, SeekOrigin.Begin);
                    BitmapImage imgsource = new BitmapImage();
                    imgsource.BeginInit();
                    imgsource.StreamSource = memorystream;
                    imgsource.EndInit();
                    ImageInPost = imgsource;
                    imagePost = value;
                }
            }
        }
        public ICommand AttachFile { get; set; }
        private bool OnAttachFile(object obj) => true;

        private void CanAttachFile(object obj)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
                openFileDialog.ShowDialog(); // показываем
                byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
                ImagePost = image_bytes;
            }
            catch
            {

            }
        }
        private BitmapImage imageInPost;
        public BitmapImage ImageInPost
        {
            get { return imageInPost; }
            set
            {
                imageInPost = value;
                OnPropertyChange("ImageInPost");
            }
        }

        private string contentInPost;
        public string ContentInPost
        {
            get { return contentInPost; }
            set
            {
                contentInPost = value;
                OnPropertyChange("ContentInPost");
            }
        }

        public ICommand DeletedPost { get; set; }
        private bool OnDeletedPost(object obj) => true; 
        private void CanDeletedPost(object obj)
        {
            MessageBox.Show(SelectedPost.info);
        }

        private static Posts selectedPost;
        public static Posts SelectedPost
        {
            get { return selectedPost; }
            set
            {
                selectedPost = value;
                //MessageBox.Show("dd");
            }
        }
    }
}
