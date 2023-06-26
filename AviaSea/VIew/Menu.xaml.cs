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
using System.Windows.Shapes;

namespace AviaSea.VIew
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btMenuAvia_Click(object sender, RoutedEventArgs e)
        {
            frContent.Content = new WindowOfTheListOfAvailableFlights();
        }

        private void btLikedAviaSea_Click(object sender, RoutedEventArgs e)
        {
            frContent.Content = new LikedIsAirTravel();
        }

        private void btProfil_Click(object sender, RoutedEventArgs e)
        {
            frContent.Content = new UserProfil();
        }

        private void btPosts_Click(object sender, RoutedEventArgs e)
        {
            frContent.Content = new Posts();

        }
        private void btExitUser_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите сменить аккаунт?", "Смена аккаунта", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Maksim Safonov\\source\\repos\\AviaSea\\AviaSea\\user.txt", false))
                {
                }
                Autorization autorization = new Autorization();
                autorization.Show();
                this.Close();
            }
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы дейстивтельно хотите выйти?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
                
        }
    }
}
