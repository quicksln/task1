using QS.Task2.Database.Entities;
using QS.Task2.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Services.Mappers
{
    public static class WeatherValuesMapper
    {
        public static WeatherValuesViewModel ToViewModel(WeatherValues weatherValues)
        {
            return new WeatherValuesViewModel
            {
                 Country = weatherValues.Country,
                 MaxWindSpeed = weatherValues.MaxWindSpeed,
                 MinTemperature = weatherValues.MinTemperature
            };
        }

        public static WeatherValues ToEntity(WeatherValuesViewModel viewModel)
        {
            return new WeatherValues
            {
                 Country = viewModel.Country,
                 MaxWindSpeed = viewModel.MaxWindSpeed,
                 MinTemperature = viewModel.MinTemperature
            };
        }
    }
}
