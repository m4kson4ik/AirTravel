using AviaSea.VIew;
using AviaSea.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AviaSea.Models
{
    public class AirTravel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string names)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(names));
            }
        }
        private string? searchToken; // идентификатор
        private string? startCity; // город отправления
        private string? startCityCode; // международный код города отправления
        private string? endCity; // город прибытия
        private string? endCityCode; //международный код города прибытия
        private DateTime? startDate; //дата и время перелёта «туда»
        private DateTime? endDate; //дата и время перелета «обратно»
        private int? price; //цена(в рублях)
        private bool? status; //cтатус

          public string? SearchToken  // идентификатор
          {
              get { return searchToken; }
              set { searchToken = value; OnPropertyChange("SearchToken"); }
          }
          public string? StartCity
          {
              get { return startCity; }
              set { startCity = value; OnPropertyChange("StartCity"); }
          } // город отправления
        
          public string? StartCityCode // международный код города отправления 
          {
              get { return startCityCode; }
              set { startCityCode = value; OnPropertyChange("StartCityCode"); }
          }
          public string? EndCity // город прибытия
          {
              get { return endCity; }
              set
              {
                  endCity = value;
                  OnPropertyChange("EndCity");
              }
          }
          
          public string? EndCityCode //международный код города прибытия
          {
              get { return endCityCode; }
              set
              {
                  endCityCode = value;
                  OnPropertyChange("EndCityCode");
              }
          }
        
          public DateTime? StartDate //дата и время перелёта «туда»
          {
              get { return startDate; }
              set
              {
                  startDate = value;
                  OnPropertyChange("StartDate");
              }
          }
          public DateTime? EndDate //дата и время перелета «обратно»
          {
              get { return endDate; }
              set
              {
                  endDate = value;
                  OnPropertyChange("EndDate");
              }
          }
          public int? Price //цена(в рублях)
          {
              get { return price; }
              set
              {
                  price = value;
                  OnPropertyChange("Price");
              }
          }
        
        public bool? isStatus
        {
            get { return status; }
            set
            {
                using (var context = new DateBase.AviaSeaContext())
                {
                    if (ViewModelWindowOfTheListOfAvailableFlights.sat != 0)
                    {
                        if (status == true)
                        {
                            var item = context.LikedAviaSeas.SingleOrDefault(s=>s.IdUser == ViewModelAutorization.id_user && s.LikedToken == SearchToken);
                            if (item != null)
                            {
                                context.LikedAviaSeas.Remove(item);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            var newitem = new DateBase.LikedAviaSea()
                            {
                                IdUser = ViewModelAutorization.id_user,
                                LikedToken = SearchToken
                            };
                            context.LikedAviaSeas.Add(newitem);
                            context.SaveChanges();
                        }
                    }
                    status = value;
                }
            }
        }
        public int? Status
        {
            set
            {
                if (value == 1)
                {
                    isStatus = true;
                }
                else
                {
                    isStatus = false;
                }
            }
        }
    }
}
