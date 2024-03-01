using Microsoft.AspNetCore.Components;
using QS.Task2.Infrastructure.Interfaces;
using QS.Task2.Infrastructure.ViewModels;

namespace QS.Task2.Weather.Pages
{
    public partial class Index
    {
        [Inject] IWeatherService WeatherService { get; set; }

        protected List<WeatherViewModel> Weathers { get; set; } = new List<WeatherViewModel>();

        protected List<ChartItem> MinTemperatures { get; set; } = new List<ChartItem>();

        protected List<ChartItem> HighestWindSpeeds { get; set; } = new List<ChartItem>();

        private bool ShowTrend { get; set; }

        private bool ShowTrendWind { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Weathers = (await WeatherService.GetAllWeathersAsync())?.ToList() ?? new List<WeatherViewModel>();

            if (Weathers.Any())
            {
                MinTemperatures = Weathers
                .GroupBy(w => new { w.Location.Country, w.Location.City })
                .Select(group =>  new ChartItem()
                {
                    Location = $"{ group.Key.Country}-{group.Key.City}",
                    Value = group.Min(w => w.Temperature),
                    LastUpdateTime = group.Min(w => w.Date)
                }).ToList();

                HighestWindSpeeds = Weathers
                    .GroupBy(w => new { w.Location.Country, w.Location.City })
                    .Select(group => new ChartItem()
                    {
                        Location = $"{group.Key.Country}-{group.Key.City}",
                        Value = group.Max(w => w.WindSpeed),
                        LastUpdateTime = group.Max(w => w.Date)
                    }).ToList();
            }
            await base.OnInitializedAsync();
        }
    }
}
