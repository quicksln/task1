using QS.Task2.Infrastructure.Models;
using System;
using System.Threading.Tasks;

namespace QS.Task2.Infrastructure.Interfaces
{
    public interface IWeatherAPIClient
    {
        /// <summary>
        /// Retrieves weather information from the external weather API based on the provided latitude and longitude.
        /// </summary>
        /// <param name="lat">The latitude of the location</param>
        /// <param name="lon">The longitude of the location</param>
        /// <returns>The weather response containing temperature, cloud cover, and wind speed</returns>
        Task<WeatherResponse?> GetRandomAPI(double lat, double lon);
    }
}
