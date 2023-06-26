using AviaSea.DateBase;
using AviaSea.Infostraction.Commands;
using Microsoft.IdentityModel.Tokens;
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

namespace AviaSea.ViewModel
{
    public class ViewModelRegistration : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string names)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(names));
            }
        }
        public ViewModelRegistration()
        {
            Registration = new LambdaCommand(OnRegistration,CanRegistration);
        }


        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChange("Login");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChange("Password");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChange("Name");
            }
        }

        private string family;
        public string Family
        {
            get { return family; }
            set
            {
                family = value;
                OnPropertyChange("Family");
            }
        }

        public ICommand Registration { get; }
        private bool CanRegistration (object obj)
        {
            if (family != null && name != null && login != null && password != null && name != "" && family != "" && login != "" && password != "") 
            {
                return true;
            }
            return false;
        }
        private void OnRegistration(object obj)
        {
            using (var context = new AviaSeaContext())
            {
                var newitem = new DateBase.User
                {
                    Family = family,
                    Name = name,
                    Password = password,
                    Login = login,
                    Img = File.ReadAllBytes("C:\\Users\\Maksim Safonov\\source\\repos\\AviaSea\\AviaSea\\Resourse\\camera_200.png"),
            };
                context.Users.Add(newitem);
                context.SaveChanges();
            }
        }
    }
}
