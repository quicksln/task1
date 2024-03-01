using Microsoft.EntityFrameworkCore;
using QS.Task2.Database;
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
    /// Service class for managing Weather
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly WeatherDbContext _context;

        public WeatherService(WeatherDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create/Add a new Weather record
        /// </summary>
        /// <param name="weatherVm"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<WeatherViewModel> CreateWeatherAsync(WeatherViewModel weatherVm)
        {
            var weather = WeatherMapper.ToEntity(weatherVm);
            weather.Location = await _context.Locations.FindAsync(weatherVm.CityId);
            if (weather.Location == null) throw new Exception("Location not found");

            _context.Weathers.Add(weather);
            await _context.SaveChangesAsync();
            return WeatherMapper.ToViewModel(weather);
        }

        /// <summary>
        /// Retrieve all Weather records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<WeatherViewModel>> GetAllWeathersAsync()
        {
            var weathers = await _context.Weathers.Include(w => w.Location).ToListAsync();
            return weathers.Select(WeatherMapper.ToViewModel);
        }

        /// <summary>
        /// Retrieve a specific Weather record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WeatherViewModel?> GetWeatherByIdAsync(int id)
        {
            var weather = await _context.Weathers.Include(w => w.Location).FirstOrDefaultAsync(w => w.Id == id);
            return weather != null ? WeatherMapper.ToViewModel(weather) : null;
        }

        /// <summary>
        /// Update a specific Weather record
        /// </summary>
        /// <param name="weatherVm"></param>
        /// <returns></returns>
        public async Task<WeatherViewModel?> UpdateWeatherAsync(WeatherViewModel weatherVm)
        {
            var weather = await _context.Weathers.FindAsync(weatherVm.Id);
            if (weather == null) return null;

            weather.Date = weatherVm.Date;
            weather.Temperature = weatherVm.Temperature;
            weather.Clouds = weatherVm.Clouds;
            weather.WindSpeed = weatherVm.WindSpeed;

            _context.Weathers.Update(weather);
            await _context.SaveChangesAsync();
            return WeatherMapper.ToViewModel(weather);
        }

        /// <summary>
        /// Delete a specific Weather record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteWeatherAsync(int id)
        {
            var weather = await _context.Weathers.FindAsync(id);
            if (weather == null) return false;

            _context.Weathers.Remove(weather);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
