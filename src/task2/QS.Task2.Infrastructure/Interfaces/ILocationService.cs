using QS.Task2.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Task2.Infrastructure.Interfaces
{
    public interface ILocationService
    {
        /// <summary>
        /// Create/Add a new Location
        /// </summary>
        /// <param name="locationVm"></param>
        /// <returns></returns>
        Task<LocationViewModel> CreateLocationAsync(LocationViewModel locationVm);

        /// <summary>
        /// Delete a specific Location by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteLocationAsync(int id);

        /// <summary>
        /// Retrieve all Locations
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LocationViewModel>> GetAllLocationsAsync();

        /// <summary>
        /// Retrieve a specific Location by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LocationViewModel?> GetLocationByIdAsync(int id);

        /// <summary>
        /// Update a specific Location
        /// </summary>
        /// <param name="locationVm"></param>
        /// <returns></returns>
        Task<LocationViewModel?> UpdateLocationAsync(LocationViewModel locationVm);

        /// <summary>
        /// Get the maximum wind speed for a given temperature.
        /// </summary>
        /// <param name="temperature"></param>
        /// <returns></returns>
        IEnumerable<WeatherValuesViewModel> GetMinTemperature(double temperature);

        /// <summary>
        /// Get the minimum temperature for a given country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        IEnumerable<WindValuesViewModel> GetMaxWindSpeed(string country);
    }
}
