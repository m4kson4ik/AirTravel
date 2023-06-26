using AviaSea.DateBase;
using AviaSea.Infostraction.Commands;
using AviaSea.Models;
using AviaSea.VIew;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AviaSea.ViewModel
{
    public class ViewModelEditingUserProfil : INotifyPropertyChanged
    {
        public Models.UserProfil User { get; set; }

        private string password;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string names)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(names));
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
                    //UpdateImage = image_bytes;
                    context.Users.Update(item);
                    context.SaveChanges();
                    OnPropertyChange("SelectedAvatar");
                }
            }
            catch
            {

            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChange("Password");
            }
        }
        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                OnPropertyChange("NewPassword");
            }
        }
        private Visibility isValidPassword;
        public Visibility IsValidPassword
        {
            get { return isValidPassword; }
            set
            {
                isValidPassword = value;
                OnPropertyChange("isValid");
            }
        }

        public ViewModelEditingUserProfil()
        {
            CommandSelectedAvatar = new LambdaCommand(CanCommandSelectedAvatar, OnCommandSelectedAvatar);
            CommandSave = new LambdaCommand(CanCommandSave,OnCommandSave);
            EditingPasswordAccept = new LambdaCommand(CanEditingPasswordAccept,OnEditingPasswordAccept);
            using (var context = new AviaSeaContext())
            {
                var item = context.Users.SingleOrDefault(s=>s.IdUser == ViewModel.ViewModelAutorization.id_user);
                var user = new Models.UserProfil
                {
                    Family = item.Family,
                    Name = item.Name,
                    Login = item.Login,
                    Password = item.Password,
                    IdUser = item.IdUser,
                    Status = item.Status,
                    Img = item.Img,
                };
                User = user;
            }
        }
        public ICommand EditingPasswordAccept { get; set; }
        private bool OnEditingPasswordAccept(object obj) => true;
        public void CanEditingPasswordAccept(object obj)
        {
            if (password == User.Password)
            {
                MessageBox.Show(Password);
                MessageBox.Show(User.Password);
                IsValidPassword = Visibility.Visible;
            }
        }
        public ICommand CommandSave { get; set; }
        private bool OnCommandSave(object obj)
        {
            if (User.Password != "" && newPassword != "")
            {
                return true;
            }
            return false;
        }
        public void CanCommandSave(object obj)
        {
            if (Password == User.Password)
            {
                using (var context = new AviaSeaContext())
                {
                    var item = context.Users.SingleOrDefault(S => S.IdUser == User.IdUser);
                    item.Name = User.Name;
                    item.Family = User.Family;
                    item.Password = newPassword ?? User.Password;
                    context.SaveChanges();
                    MessageBox.Show("Изменения успешно внесены!");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
            }
        }
    }
}
