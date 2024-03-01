using QS.Task2.Database.Entities;
using QS.Task2.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Services.Mappers
{
    public static class LocationMapper
    {
        public static LocationViewModel ToViewModel(Location location)
        {
            return new LocationViewModel
            {
                Id = location.Id,
                Lat = location.Lat,
                Lon = location.Lon,
                Country = location.Country,
                City = location.City
            };
        }

        public static Location ToEntity(LocationViewModel viewModel)
        {
            return new Location
            {
                Id = viewModel.Id,
                Lat = viewModel.Lat,
                Lon = viewModel.Lon,
                Country = viewModel.Country,
                City = viewModel.City
            };
        }
    }

}
