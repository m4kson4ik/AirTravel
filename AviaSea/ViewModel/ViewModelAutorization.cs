using AviaSea.DateBase;
using AviaSea.Infostraction.Commands;
using AviaSea.VIew;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AviaSea.ViewModel
{
    public class ViewModelAutorization
    {
        public delegate void ShowWindow();
        public event ShowWindow? ShowWindowAcceptLogin;
        public static int id_user;
        public ViewModelAutorization()
        {
            CommandVxod = new LambdaCommand(CanCommandVxod, OnCommandVxod);
            CheckedIsAutorizationUser();
        }

        private bool savePasswordCheckBox;
        public bool SavePasswordCheckBox
        {
            get { return savePasswordCheckBox; }
            set
            {
                savePasswordCheckBox = value;
            }
        }

        public string[] contentline = new string[3];
        private string path = "C:\\Users\\Maksim Safonov\\source\\repos\\AviaSea\\AviaSea\\user.txt";
        async private void CheckedIsAutorizationUser()
        {

            // асинхронное чтение
            string[] line = new string[3];
            using (StreamReader reader = new StreamReader(path))
            {
                int i = 0;
                    while ((contentline[i] = await reader.ReadLineAsync()) != null)
                    {
                        if (i == 2)
                        {
                            break;
                        }
                        i++;
                    }
            }
            CanGoingVxod();
        }

        private string login;
        public string LoginUser
        {
            get { return login; }
            set { login = value; }
        }

        private string password;
        public string PasswordUser
        {
            get { return password; }
            set { password = value; }
        }
        public ICommand CommandVxod { get; }
        private bool OnCommandVxod(object obj)
        {
            if (login != "" && password != "" && login != null && password != null )
            {
                return true;
            }
            return false;
        }
        private void CanCommandVxod(object obj)
        {
            try
            {
                using (var context = new AviaSeaContext())
                {
                    var item = context.Users.SingleOrDefault(s => s.Login == login && s.Password == password);
                    if (item != null)
                    {
                        ShowWindowAcceptLogin?.Invoke();
                        id_user = (int)item.IdUser;
                        if (SavePasswordCheckBox == true)
                        {
                            SaveCheckPassword(item.Login, item.Password);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неправильные данные!");
                    }    
                }
            }
            catch
            {

            }
        }
        async private void SaveCheckPassword(string login, string password)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync("1");
                await writer.WriteAsync($"{login}\n{password}");
            }
        }

        private void CanGoingVxod()
        {
            if (contentline[0] != "1")
            {
            }
            else
            {
                using (var context = new AviaSeaContext())
                {
                    var item = context.Users.SingleOrDefault(s=> s.Login == contentline[1] && s.Password == contentline[2]);
                    if (item != null)
                    {
                        ShowWindowAcceptLogin?.Invoke();
                        id_user = (int)item.IdUser;
                    }
                }
            }          
        }
    }
}
