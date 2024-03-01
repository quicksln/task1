using QS.Task2.Database;
using QS.Task2.Database.Entities;
using QS.Task2.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Services
{
    /// <summary>
    /// Service class for seeding the database with initial data.
    /// </summary>
    public class SeedingService : ISeedingService
    {
        private readonly WeatherDbContext _weatherDbContext;
        public SeedingService(WeatherDbContext weatherDbContext)
        {
            _weatherDbContext = weatherDbContext;
        }

        /// <summary>
        /// Seeds the database with initial data if the configuration is not already seeded.
        /// </summary>
        public void Seed()
        {
            var configuration = _weatherDbContext.Configuration.FirstOrDefault();
            if (configuration == null)
            {
                _weatherDbContext.Locations.AddRange(new List<Location>
                    {
                        // Poland - Warsaw (capital) and Krakow (second largest)
                        new Location { Country = "Poland", City = "Warsaw", Lat = 52.229676, Lon = 21.012229 },
                        new Location { Country = "Poland", City = "Krakow", Lat = 50.064650, Lon = 19.944980 },

                        // Sweden - Stockholm (capital) and Gothenburg (second largest)
                        new Location { Country = "Sweden", City = "Stockholm", Lat = 59.329323, Lon = 18.068581 },
                        new Location { Country = "Sweden", City = "Gothenburg", Lat = 57.708870, Lon = 11.974560 },

                        // Norway - Oslo (capital) and Bergen (second largest)
                        new Location { Country = "Norway", City = "Oslo", Lat = 59.913869, Lon = 10.752245 },
                        new Location { Country = "Norway", City = "Bergen", Lat = 60.391263, Lon = 5.322054 },

                        // United Kingdom - London (capital) and Birmingham (second largest)
                        new Location { Country = "United Kingdom", City = "London", Lat = 51.507351, Lon = -0.127758 },
                        new Location { Country = "United Kingdom", City = "Birmingham", Lat = 52.486243, Lon = -1.890401 },

                        // United States - Washington D.C. (capital) and Los Angeles (second largest)
                        new Location { Country = "United States", City = "Washington D.C.", Lat = 38.907192, Lon = -77.036871 },
                        new Location { Country = "United States", City = "Los Angeles", Lat = 34.052234, Lon = -118.243685 },
                    });
                _weatherDbContext.Configuration.Add(new Configuration { IsSeeded = true });

                _weatherDbContext.SaveChanges();
            }
        }
    }
}
