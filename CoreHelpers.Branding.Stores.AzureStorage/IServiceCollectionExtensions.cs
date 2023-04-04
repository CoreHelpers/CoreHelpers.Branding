using System;
using CoreHelpers.Branding.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreHelpers.Branding.Stores.AzureStorage
{
    public static class IServiceCollectionExtensions
    {        
        public static IServiceCollection AddBrandingServices4AzureStorage(this IServiceCollection services, string connectionString, string container)
        {
            services.AddScoped<IBrandingStore>((serviceProvider) => new AzureStorageBrandingStore(connectionString, container));
            return services;
        }

        public static IServiceCollection AddBrandingServices4AzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            // search the configuration
            var connectionString = configuration.GetValue<string>("Branding:Stores:AzureStorage:ConnectionString");
            var container = configuration.GetValue<string>("Branding:Stores:AzureStorage:Container");

            // setup
            return services.AddBrandingServices4AzureStorage(connectionString, container);
        }
    }
}

