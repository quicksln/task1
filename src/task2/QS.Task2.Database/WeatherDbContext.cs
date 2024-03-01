using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QS.Task2.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Database
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<Configuration> Configuration { get; set; }

        #region Stored Procedures

        [NotMapped]
        public DbSet<WeatherValues> WeatherValues { get; set; }

        [NotMapped]
        public DbSet<WindValues> WindValues { get; set; }

        public virtual IEnumerable<WeatherValues> GetMinTemperature(double temperature)
        {
            return WeatherValues.FromSqlRaw("exec [dbo].[stp_Weather_GetMinTemperature] @Temperature = {0}", temperature).ToList();
        }

        public virtual IEnumerable<WindValues> GetMaxWindSpeed(string country)
        {
            return WindValues.FromSqlRaw("exec [dbo].[stp_Weather_GetMaxWindSpeed] @CountName = {0}", country).ToList();
        }

        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherValues>().HasNoKey();

            modelBuilder.Entity<WindValues>().HasNoKey();

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Weathers)
                .WithOne(w => w.Location)
                .HasForeignKey(w => w.CityId);

            // Index on the 'Country' property of the Location entity
            modelBuilder.Entity<Location>()
                .HasIndex(l => l.Country);

            // Index on the 'Temperature' property of the Weather entity
            modelBuilder.Entity<Weather>()
                .HasIndex(w => w.Temperature);
        }

        #endregion
    }
}
