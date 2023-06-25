using AviaSea.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AviaSea.ViewModel
{
    public class ViewModelLikedAirTravel
    {
        public ObservableCollection<AirTravel> CollectionLikedAirTravel { get; } = new ObservableCollection<AirTravel>();
        private bool ischecked;

        public bool isCheckeds
        {
            get { return ischecked; }
            set
            {
                ischecked = value;
                MessageBox.Show("Click");
            }
        }
        public ViewModelLikedAirTravel()
        {
            using (StreamReader r = new StreamReader("C:\\Users\\Maksim Safonov\\Downloads\\avia1.json"))
            {
                string json = r.ReadToEnd();
                //ObservableCollection<AirTravel> CollectionAirTravel = new ObservableCollection<AirTravel>();
                dynamic array = JsonConvert.DeserializeObject(File.ReadAllText("C:\\Users\\Maksim Safonov\\Downloads\\avia.json"));
                foreach (var item in array["data"])
                {
                    using (var context = new DateBase.AviaSeaContext())
                    {
                        string like = item["searchToken"];
                        var liked = context.LikedAviaSeas.SingleOrDefault(s => s.LikedToken == like && s.IdUser == ViewModelAutorization.id_user);
                        bool statusLike = false;
                        ViewModelWindowOfTheListOfAvailableFlights.sat = 0;
                        if (liked != null)
                        {
                            statusLike = true;
                            AirTravel airTravel = new AirTravel { StartCity = item["startCity"], SearchToken = item["searchToken"], Price = item["price"], EndCity = item["endCity"], EndCityCode = item["endCityCode"], EndDate = item["endDate"], StartCityCode = item["startCityCode"], StartDate = item["startDate"], isStatus = statusLike };
                            CollectionLikedAirTravel.Add(airTravel);
                        }
                    }
                }
            }
            ViewModelWindowOfTheListOfAvailableFlights.sat = 1;
        }
    }
}
