using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SB.Shop.AzureFunctionApp.Helpers;
using System.IO;

[assembly: FunctionsStartup(typeof(SB.Shop.AzureFunctionApp.Program))]

namespace SB.Shop.AzureFunctionApp
{
    internal class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IOrganizationServiceConfigurator, OrganizationServiceConfigurator>();
            builder.Services.AddTransient<IAzureQuequeClientService, AzureQuequeClientService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);

            var context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    }
}