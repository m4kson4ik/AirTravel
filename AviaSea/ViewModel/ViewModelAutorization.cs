using AviaSea.DateBase;
using AviaSea.Infostraction.Commands;
using AviaSea.VIew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AviaSea.ViewModel
{
    public class ViewModelAutorization
    {
        public static int id_user;
        public ViewModelAutorization()
        {
            CommandVxod = new LambdaCommand(CanCommandVxod, OnCommandVxod);
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
                        id_user = (int)item.IdUser;
                        Menu menu = new Menu();
                        menu.ShowDialog();
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
    }
}
