using AviaSea.DateBase;
using AviaSea.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

using System.Drawing.Imaging;

using System.IO;

using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;

namespace AviaSea.VIew
{
    /// <summary>
    /// Логика взаимодействия для UserProfil.xaml
    /// </summary>
    public partial class UserProfil : Page
    {
        public UserProfil()
        {
            InitializeComponent();
            UpdateImage();
        }

        private void btSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
                openFileDialog.ShowDialog(); // показываем
                byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
                using (var context = new AviaSeaContext())
                {
                    var item = context.Users.SingleOrDefault(s => s.IdUser == ViewModelAutorization.id_user);
                    item.Img = image_bytes;
                    context.Users.Update(item);
                    context.SaveChanges();
                    UpdateImage();
                }
            }
            catch
            {

            }
        }
        private void UpdateImage()
        {
            using (var context = new AviaSeaContext())
            {
                var item = context.Users.SingleOrDefault(s => s.IdUser == ViewModelAutorization.id_user);
                tbName.Text = item.Name;
                tbFamily.Text = item.Family;
                if (item.Img != null)
                {
                    MemoryStream memorystream = new MemoryStream();
                    memorystream.Write(item.Img, 0, (int)item.Img.Length);
                    memorystream.Seek(0, SeekOrigin.Begin);
                    BitmapImage imgsource = new BitmapImage();
                    imgsource.BeginInit();
                    imgsource.StreamSource = memorystream;
                    imgsource.EndInit();
                    image.Source = imgsource; // реальный Image
                }
            }
        }
        private void btSelectыed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch
            {

            }
        }

        private void addFotoanPost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
                openFileDialog.ShowDialog(); // показываем
                byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
                imagePost = image_bytes;
                MemoryStream memorystream = new MemoryStream();
                memorystream.Write(image_bytes, 0, (int)image_bytes.Length);
                memorystream.Seek(0, SeekOrigin.Begin);
                BitmapImage imgsource = new BitmapImage();
                imgsource.BeginInit();
                imgsource.StreamSource = memorystream;
                imgsource.EndInit();
                imgPosts.Source = imgsource;
            }
            catch
            {

            }
        }
        public byte[] imagePost;
        private void btCreatePost_Click(object sender, object e)
        {
            //byte[] image_bytes = File.ReadAllBytes(imgPosts.Source.ToString()); // получаем байты выбранного файла
            using(var context = new AviaSeaContext())
            {
                var newitem = new DateBase.Post()
                {
                    IdUser = ViewModelAutorization.id_user,
                    Info = tbInformation.Text,
                    ImagePosts = imagePost,
                    KolvoSee = 0,
                };
                context.Posts.Add(newitem);
                context.SaveChanges();
            }
        }
    }
}
