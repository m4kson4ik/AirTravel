using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AviaSea.Models
{
    public class UserProfil : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string names)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(names));
            }
        }
        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChange("Name");
            }
        }

        public string? Family
        {
            get { return family; }
            set
            {
                family = value;
                OnPropertyChange("Family");
            }
        }

        public string? Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChange("Status");
            }
        }


        public byte[]? Img
        {
            get { return img; }
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
                    imageUser = imgsource;
                }
            }
        }
        public BitmapImage imageUser { get; set; }
        public int IdUser
        {
            get { return idUser; }
            set
            {
                idUser = value;
                OnPropertyChange("Id_User");
            }
        }

        public string? Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChange("Login");
            }
        }

        public string? Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChange("Password");
            }
        }

        private string? name;
        private string? family;

        private string? status;

        private byte[]? img;

        private int idUser;

        private string? login;

        private string? password;
    }
}
