using Microsoft.EntityFrameworkCore;
using QS.Task2.Database;
using QS.Task2.Database.Entities;
using QS.Task2.Infrastructure.Interfaces;
using QS.Task2.Infrastructure.ViewModels;
using QS.Task2.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Services
{
    /// <summary>
    /// Service class for managing locations.
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly WeatherDbContext _context;

        public LocationService(WeatherDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create/Add a new Location
        /// </summary>
        /// <param name="locationVm"></param>
        /// <returns></returns>
        public async Task<LocationViewModel> CreateLocationAsync(LocationViewModel locationVm)
        {
            var location = LocationMapper.ToEntity(locationVm);
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return LocationMapper.ToViewModel(location);
        }

        /// <summary>
        /// Retrieve all Locations
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LocationViewModel>> GetAllLocationsAsync()
        {
            var locations = await _context.Locations.ToListAsync();
            return locations.Select(LocationMapper.ToViewModel);
        }

        /// <summary>
        /// Retrieve a specific Location by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LocationViewModel?> GetLocationByIdAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            return location != null ? LocationMapper.ToViewModel(location) : null;
        }

        /// <summary>
        /// Update a specific Location
        /// </summary>
        /// <param name="locationVm"></param>
        /// <returns></returns>
        public async Task<LocationViewModel?> UpdateLocationAsync(LocationViewModel locationVm)
        {
            var location = await _context.Locations.FindAsync(locationVm.Id);
            if (location == null) return null;

            location.Lat = locationVm.Lat;
            location.Lon = locationVm.Lon;
            location.Country = locationVm.Country;
            location.City = locationVm.City;

            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return LocationMapper.ToViewModel(location);
        }

        /// <summary>
        /// Delete a specific Location by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteLocationAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null) return false;

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Get the minimum temperature for a given country.
        /// </summary>
        /// <param name="temperature"></param>
        /// <returns></returns>
        public IEnumerable<WeatherValuesViewModel> GetMinTemperature(double temperature)
        {
            var values = _context.GetMinTemperature(temperature);
            return values.Select(WeatherValuesMapper.ToViewModel);
        }

        /// <summary>
        /// Get the maximum wind speed for a given temperature.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public IEnumerable<WindValuesViewModel> GetMaxWindSpeed(string country)
        {
            var values = _context.GetMaxWindSpeed(country);
            return values.Select(WindValuesMapper.ToViewModel);
        }
    }

}
