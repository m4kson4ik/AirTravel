using AviaSea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaSea.ViewModel
{
    public class ViewModelDetailInfoAvia
    {
        public AirTravel AirTravel { get; set; } = ViewModelWindowOfTheListOfAvailableFlights.SelectedAir;
        
    }
}
