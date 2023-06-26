﻿using AviaSea.ViewModel;
using System;
using System.Collections.Generic;
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

namespace AviaSea.VIew
{
    /// <summary>
    /// Логика взаимодействия для Posts.xaml
    /// </summary>
    public partial class Posts : Page
    {
        public Posts()
        {
            InitializeComponent();
            ((ViewModelAllPosts)DataContext).OpenWindow += HiddenAllInfo;
            btPost.Visibility = Visibility.Hidden;
        }

        private void HiddenAllInfo()
        {
            frContent.Content = new AllInfoPost();
            btPost.Visibility = Visibility.Visible;
        }

        private void btPost_Click(object sender, RoutedEventArgs e)
        {
            frContent.Content = null;
            btPost.Visibility = Visibility.Hidden;
        }
    }
}
