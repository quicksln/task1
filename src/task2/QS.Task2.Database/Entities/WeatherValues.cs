using System;
using System.Linq;

namespace QS.Task2.Database.Entities
{
    public class WeatherValues
    {
        public string? Country { get; set; }

        public double? MinTemperature { get; set; }

        public double? MaxWindSpeed { get; set; }

    }

    public class WindValues
    {
        public string? Country { get; set; }

        public double? MaxWindSpeed { get; set; }

    }
}
