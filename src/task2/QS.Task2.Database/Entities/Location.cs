using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Database.Entities
{
    
    ///<summary>
    /// Represents a location with latitude, longitude, country, and city information.
    /// </summary>
    public class Location
    {
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        // Navigation property
        public ICollection<Weather> Weathers { get; set; }
    }

}
