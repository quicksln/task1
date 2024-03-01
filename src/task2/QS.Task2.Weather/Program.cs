using mBank.Website.Base.BackgroundServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using QS.Task1.APIChecker.Configuration;
using QS.Task2.Database;
using QS.Task2.Infrastructure.Interfaces;
using QS.Task2.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WeatherDbContext>(options =>
      options.UseSqlServer(connectionString,
            migration => migration.MigrationsAssembly("QS.Task2.Database")),
      ServiceLifetime.Scoped
);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ISeedingService, SeedingService>();
builder.Services.AddScoped<IWeatherAPIClient, WeatherAPIClient>();

builder.Services.AddHostedService<WeatherCollector>();

var app = builder.Build();

try
{
    using (var serviceScope = app.Services.CreateScope())
    {
        // apply migrations to database
        serviceScope.ServiceProvider.GetRequiredService<WeatherDbContext>().Database.Migrate();
    }

    using (var serviceScope = app.Services.CreateScope())
    {
        // seed database with default data
        var seedingService = serviceScope.ServiceProvider.GetRequiredService<ISeedingService>();
        seedingService.Seed();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
