using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QS.Task1.APIChecker.Configuration;
using QS.Task1.APIChecker.Interfaces;
using QS.Task1.Services;
using QS.Task1.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

[assembly: FunctionsStartup(typeof(QS.Task1.APIChecker.Startup))]

namespace QS.Task1.APIChecker
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            //adding keys via environment variables and Azure Key Vault
            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IAPIClient, APIClient>();
            builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();

            FunctionsHostBuilderContext context = builder.GetContext();
        }
    }
}
