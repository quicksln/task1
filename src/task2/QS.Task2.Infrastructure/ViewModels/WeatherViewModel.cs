using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Infrastructure.ViewModels
{
    public class WeatherViewModel
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public DateTime Date { get; set; }
        public double? Temperature { get; set; }
        public int? Clouds { get; set; }
        public double? WindSpeed { get; set; }

        public LocationViewModel Location { get; set; }
    }

}
