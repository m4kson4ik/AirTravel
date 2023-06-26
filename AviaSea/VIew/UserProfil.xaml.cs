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
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление записи", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (var context = new AviaSeaContext())
                {
                    var post = context.Posts.SingleOrDefault(s=> VIewModelUserProfil.SelectedPost.PostId == s.IdPosts);
                    context.Posts.Remove(post);
                    context.SaveChanges();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditingUserProfil editingUserProfil = new EditingUserProfil();
            editingUserProfil.ShowDialog();
        }
    }
}
