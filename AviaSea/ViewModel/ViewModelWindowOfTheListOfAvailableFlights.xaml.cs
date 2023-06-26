using AviaSea.Infostraction.Commands;
using AviaSea.Models;
using AviaSea.VIew;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AviaSea.ViewModel
{
    public class ViewModelWindowOfTheListOfAvailableFlights : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string names)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(names));
            }
        }
        public ICommand CommandSelected { get; }
        private bool OnCommandSelected(object obj) => true;
        private void CanCommandSelected(object obj)
        {
            MessageBox.Show("ds");
        }

        private static AirTravel selectedAir;
        public static AirTravel SelectedAir
        {
            get { return selectedAir; }
            set
            {
                selectedAir = value;
                DetailInfoInAvia viewModelDetailAllInfoPosts = new DetailInfoInAvia();
                viewModelDetailAllInfoPosts.Show();
            }
        }

        public static int sat;
        public ObservableCollection<AirTravel> CollectionAirTravel { get;} = new ObservableCollection<AirTravel>();
        public ViewModelWindowOfTheListOfAvailableFlights()
        {
            CommandSelected = new LambdaCommand(CanCommandSelected, OnCommandSelected);
            using (StreamReader r = new StreamReader("C:\\Users\\Maksim Safonov\\Downloads\\avia1.json"))
            {
                string json = r.ReadToEnd();
                //ObservableCollection<AirTravel> CollectionAirTravel = new ObservableCollection<AirTravel>();
                dynamic array = JsonConvert.DeserializeObject(File.ReadAllText("C:\\Users\\Maksim Safonov\\Downloads\\avia.json"));
                foreach(var item in array["data"])
                {
                    using(var context = new DateBase.AviaSeaContext())
                    {
                        string like = item["searchToken"];
                        var liked = context.LikedAviaSeas.SingleOrDefault(s=>s.LikedToken == like && s.IdUser == ViewModelAutorization.id_user);
                        bool statusLike = false;
                        sat = 0;
                        if (liked != null)
                        {
                            statusLike = true;
                        }
                        AirTravel airTravel = new AirTravel { StartCity = item["startCity"], SearchToken = item["searchToken"], Price = item["price"], EndCity = item["endCity"], EndCityCode = item["endCityCode"], EndDate = item["endDate"], StartCityCode = item["startCityCode"], StartDate = item["startDate"], isStatus = statusLike };
                        CollectionAirTravel.Add(airTravel);
                    }
                }
                    //MessageBox.Show(CollectionAirTravel[2].searchToken.ToString());
            }
            sat = 1;
        }
    }
}
