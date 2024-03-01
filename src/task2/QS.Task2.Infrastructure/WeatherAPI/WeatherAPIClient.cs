using QS.Task2.Infrastructure.Interfaces;
using QS.Task2.Infrastructure.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace QS.Task1.APIChecker.Configuration
{
    /// <summary>
    /// API Client execute calls to external weather API
    /// </summary>
    public class WeatherAPIClient : IWeatherAPIClient
    {
        private readonly HttpClient _client;

        public WeatherAPIClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Retrieves weather information from the external weather API based on the provided latitude and longitude.
        /// </summary>
        /// <param name="lat">The latitude of the location</param>
        /// <param name="lon">The longitude of the location</param>
        /// <returns>The weather response containing temperature, cloud cover, and wind speed</returns>
        public async Task<WeatherResponse?> GetRandomAPI(double lat, double lon)
        {
            string apiUri = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,cloud_cover,wind_speed_10m";
            var response = await _client.GetAsync(apiUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<WeatherResponse>(content);
                return result;
            }
            return null;
        }
    }
}
