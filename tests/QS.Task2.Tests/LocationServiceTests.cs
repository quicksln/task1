using Microsoft.EntityFrameworkCore;
using QS.Task2.Database.Entities;
using QS.Task2.Database;
using QS.Task2.Infrastructure.ViewModels;
using QS.Task2.Services;

namespace QS.Task2.Tests;
public class LocationServiceTests
{
    private WeatherDbContext _context;
    private LocationService _service;
    private Location _location;

    public LocationServiceTests()
    {
        // Use a clean instance of context for each test
        var options = new DbContextOptionsBuilder<WeatherDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new WeatherDbContext(options);
        _service = new LocationService(_context);

        // Add a sample location
        _location = new Location { Id = 1, Country = "Test", City = "TestCity", Lat = 1.0, Lon = 1.0 };
        _context.Locations.Add(_location);
        _context.SaveChanges();
    }

    [Fact]
    public async void CreateLocationAsync_ShouldCreateLocation()
    {
        // Arrange
        var locationVm = new LocationViewModel
        {
            Lat = 50.85,
            Lon = 4.35,
            Country = "Belgium",
            City = "Brussels"
        };

        // Act
        var result = await _service.CreateLocationAsync(locationVm);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(locationVm.Lat, result.Lat);
        Assert.Equal(locationVm.Lon, result.Lon);
        Assert.Equal(locationVm.Country, result.Country);
        Assert.Equal(locationVm.City, result.City);
    }

    [Fact]
    public async void GetAllLocationsAsync_ShouldReturnAllLocations()
    {
        // Act
        var result = await _service.GetAllLocationsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result); 
    }

    [Fact]
    public async void GetLocationByIdAsync_ShouldReturnLocation()
    {
        // Act
        var result = await _service.GetLocationByIdAsync(_location.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_location.Id, result.Id);
    }

    [Fact]
    public async void UpdateLocationAsync_ShouldUpdateLocation()
    {
        // Arrange
        var updatedLocationVm = new LocationViewModel
        {
            Id = _location.Id,
            Lat = 51.21,
            Lon = 3.22,
            Country = "Belgium",
            City = "Bruges"
        };

        // Act
        var result = await _service.UpdateLocationAsync(updatedLocationVm);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedLocationVm.Lat, result.Lat);
        Assert.Equal(updatedLocationVm.Lon, result.Lon);
        Assert.Equal(updatedLocationVm.Country, result.Country);
        Assert.Equal(updatedLocationVm.City, result.City);
    }

    [Fact]
    public async void DeleteLocationAsync_ShouldDeleteLocation()
    {
        // Act
        var result = await _service.DeleteLocationAsync(_location.Id);

        // Assert
        Assert.True(result);
    }
}
