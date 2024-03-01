using QS.Task2.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Task2.Infrastructure.Interfaces
{
    public interface IWeatherService
    {
        /// <summary>
        /// Create/Add a new Weather record
        /// </summary>
        /// <param name="weatherVm"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<WeatherViewModel> CreateWeatherAsync(WeatherViewModel weatherVm);

        /// <summary>
        /// Delete a specific Weather record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteWeatherAsync(int id);

        /// <summary>
        /// Retrieve all Weather records
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<WeatherViewModel>> GetAllWeathersAsync();

        /// <summary>
        /// Retrieve a specific Weather record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WeatherViewModel?> GetWeatherByIdAsync(int id);

        /// <summary>
        /// Update a specific Weather record
        /// </summary>
        /// <param name="weatherVm"></param>
        /// <returns></returns>
        Task<WeatherViewModel?> UpdateWeatherAsync(WeatherViewModel weatherVm);
    }
}
