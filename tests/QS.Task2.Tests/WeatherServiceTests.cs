using Microsoft.EntityFrameworkCore;
using Moq;
using QS.Task2.Database;
using QS.Task2.Database.Entities;
using QS.Task2.Infrastructure.ViewModels;
using QS.Task2.Services;
using System;
using System.Linq;
using Xunit;

namespace QS.Task2.Tests;
public class WeatherServiceTests
{
    private WeatherDbContext _context;
    private WeatherService _service;

    private Location _location;
    public WeatherServiceTests()
    {
        // Use a clean instance of context for each test
        var options = new DbContextOptionsBuilder<WeatherDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new WeatherDbContext(options);
        _service = new WeatherService(_context);

        // Add a sample location
        _location = new Location { Id = 1, Country = "Test", City = "TestCity", Lat = 1.0, Lon = 1.0 };
        _context.Locations.Add(_location);
        _context.SaveChanges();
    }

    [Fact]
    public async void CreateWeatherAsync_ShouldCreateWeather()
    {
        // Arrange
        var weatherVm = new WeatherViewModel
        {
            CityId = 1,
            Date = DateTime.Now,
            Temperature = 20,
            Clouds = 88,
            WindSpeed = 5
        };

        // Act
        var result = await _service.CreateWeatherAsync(weatherVm);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(weatherVm.CityId, result.CityId);
        Assert.Equal(weatherVm.Date, result.Date);
        Assert.Equal(weatherVm.Temperature, result.Temperature);
        Assert.Equal(weatherVm.Clouds, result.Clouds);
        Assert.Equal(weatherVm.WindSpeed, result.WindSpeed);
    }

    [Fact]
    public async void GetAllWeathersAsync_ShouldReturnAllWeathers()
    {
        // Arrange
        var weather = await _service.CreateWeatherAsync(new WeatherViewModel
        {
            CityId = _location.Id,
            Date = DateTime.Now,
            Temperature = 20,
            Clouds = 88,
            WindSpeed = 5
        });

        // Act
        var result = await _service.GetAllWeathersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result); // As we added only one weather record in the constructor
    }

    [Fact]
    public async void GetWeatherByIdAsync_ShouldReturnWeather()
    {
        // Arrange
        var weather = await _service.CreateWeatherAsync(new WeatherViewModel
        {
            CityId = _location.Id,
            Date = DateTime.Now,
            Temperature = 20,
            Clouds = 88,
            WindSpeed = 5
        });

        // Act
        var result = await _service.GetWeatherByIdAsync(weather.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(weather.Id, result.Id);
    }

    [Fact]
    public async void UpdateWeatherAsync_ShouldUpdateWeather()
    {
        // Arrange
        var weather = await _service.CreateWeatherAsync(new WeatherViewModel
        {
            CityId = _location.Id,
            Date = DateTime.Now,
            Temperature = 20,
            Clouds = 88,
            WindSpeed = 5
        });

        var updatedWeatherVm = new WeatherViewModel
        {
            Id = weather.Id,
            CityId = _location.Id,
            Date = DateTime.Now,
            Temperature = 25,
            Clouds = 88,
            WindSpeed = 3
        };

        // Act
        var result = await _service.UpdateWeatherAsync(updatedWeatherVm);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedWeatherVm.Temperature, result.Temperature);
        Assert.Equal(updatedWeatherVm.Clouds, result.Clouds);
        Assert.Equal(updatedWeatherVm.WindSpeed, result.WindSpeed);
    }

    [Fact]
    public async void DeleteWeatherAsync_ShouldDeleteWeather()
    {
        // Arrange
        var weather = await _service.CreateWeatherAsync(new WeatherViewModel
        {
            CityId = _location.Id,
            Date = DateTime.Now,
            Temperature = 20,
            Clouds = 88,
            WindSpeed = 5
        });

        // Act
        var result = await _service.DeleteWeatherAsync(weather.Id);

        // Assert
        Assert.True(result);
    }
}