using QS.Task2.Database.Entities;
using QS.Task2.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Services.Mappers
{
    public static class WeatherMapper
    {
        public static WeatherViewModel ToViewModel(Weather weather)
        {
            var result = new WeatherViewModel
            {
                Id = weather.Id,
                CityId = weather.CityId,
                Date = weather.Date,
                Temperature = weather.Temperature,
                Clouds = weather.Clouds,
                WindSpeed = weather.WindSpeed,
            };
            if (weather.Location != null)
            {
                result.Location = LocationMapper.ToViewModel(weather.Location);
            }

            return result;
        }

        public static Weather ToEntity(WeatherViewModel viewModel)
        {
            var result = new Weather
            {
                Id = viewModel.Id,
                CityId = viewModel.CityId,
                Date = viewModel.Date,
                Temperature = viewModel.Temperature,
                Clouds = viewModel.Clouds,
                WindSpeed = viewModel.WindSpeed,
            };

            if (viewModel.Location != null)
            {
                result.Location = LocationMapper.ToEntity(viewModel.Location);
            }

            return result;
        }
    }

}
