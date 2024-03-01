using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Infrastructure.ViewModels
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

}
