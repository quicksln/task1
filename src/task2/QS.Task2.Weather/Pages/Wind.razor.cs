using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using QS.Task2.Infrastructure.Interfaces;
using QS.Task2.Infrastructure.ViewModels;

namespace QS.Task2.Weather.Pages
{
    public partial class Wind
    {
        [Inject] ILocationService LocationService { get; set; }

        protected List<LocationViewModel> Locations { get; set; } = new List<LocationViewModel>();

        protected List<string> CountryNames { get; set; } = new List<string>();

        protected List<WindValuesViewModel> MaxWindSpeeds { get; set; } = new List<WindValuesViewModel>();

        protected List<WeatherValuesViewModel> MinTemperature { get; set; } = new List<WeatherValuesViewModel>();

        protected override async Task OnInitializedAsync()
        {
            Locations = (await LocationService.GetAllLocationsAsync())?.ToList() ?? new List<LocationViewModel>();

            if (Locations.Any())
            {
                CountryNames = Locations.Select(l => l.Country).Distinct().ToList();
            }
            await base.OnInitializedAsync();
        }

        protected async Task ShowMaxWindSpeed(object selectedCountry)
        {
            MaxWindSpeeds = selectedCountry != null ? LocationService.GetMaxWindSpeed(selectedCountry.ToString()).ToList()
                : new List<WindValuesViewModel>();

            await Task.CompletedTask;
        }

        protected async Task ShowMinTemp(double selectedTemp)
        {
            MinTemperature = LocationService.GetMinTemperature(selectedTemp).ToList();

            await Task.CompletedTask;
        }
    }
}
