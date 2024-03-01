using Microsoft.Extensions.Hosting;
using QS.Task2.Infrastructure.Interfaces;
using QS.Task2.Infrastructure.Models;
using QS.Task2.Infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace mBank.Website.Base.BackgroundServices
{
    /// <summary>
    /// Represents a background service that collects weather data at regular intervals.
    /// </summary>
    public class WeatherCollector : BackgroundService
    {
        private readonly ILogger<WeatherCollector> _logger;
        private readonly IServiceProvider _services;
        private Timer? _timer = null;

        public WeatherCollector(IServiceProvider services, ILogger<WeatherCollector> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const int _interval = 15; // minutes interval between API calls required by Weather API documentation
            if (_timer is null)
            {
                _timer = new Timer(async (s) =>
                {
                    await DoWork(s);
                }, null, TimeSpan.Zero, TimeSpan.FromMinutes(_interval));
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// Executes the weather data collection task at regular intervals.
        /// </summary>
        private async Task DoWork(object? state)
        {
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var weatherAPIClient = scope.ServiceProvider.GetRequiredService<IWeatherAPIClient>();
                    var locationService = scope.ServiceProvider.GetRequiredService<ILocationService>();
                    var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();

                    var locations = (await locationService.GetAllLocationsAsync())?.ToList() ?? new List<LocationViewModel>();
                    if (locations.Any())
                    {
                        Task<(WeatherResponse? Weather, LocationViewModel location)>[] tasks = GetWeatherCalls(weatherAPIClient, locations);

                        await Task.WhenAll(tasks);

                        foreach (var task in tasks)
                        {
                            await AddWeather(weatherService, task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while collecting weather data.");
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task AddWeather(IWeatherService weatherService, Task<(WeatherResponse? Weather, LocationViewModel location)> task)
        {
            var result = task.Result;
            if (result.Weather != null && result.Weather.Current != null)
            {
                var weatherVm = new WeatherViewModel
                {
                    CityId = result.location.Id,
                    Clouds = result.Weather.Current!.CloudCover,
                    Temperature = result.Weather.Current!.Temperature2m,
                    WindSpeed = result.Weather.Current.WindSpeed10m,
                    Date = DateTime.Now
                };
                await weatherService.CreateWeatherAsync(weatherVm);
            }
        }

        private static Task<(WeatherResponse? Weather, LocationViewModel location)>[] GetWeatherCalls(IWeatherAPIClient weatherAPIClient, List<LocationViewModel> locations)
        {
            Task<(WeatherResponse? Weather, LocationViewModel location)>[] tasks =
                new Task<(WeatherResponse? Weather, LocationViewModel location)>[locations.Count()];
            for (int i = 0; i < locations.Count(); i++)
            {
                var location = locations[i];
                tasks[i] = Task.Run<(WeatherResponse? Weather, LocationViewModel location)>(async () =>
                {
                    var weather = await weatherAPIClient.GetRandomAPI(location.Lat, location.Lon);
                    return (weather, location);

                }, cancellationToken: CancellationToken.None);
            }

            return tasks;
        }
    }
}
