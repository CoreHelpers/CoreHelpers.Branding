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
            return services.AddBrandingServices4AzureStorage(connectionString, container, true);
        }
        
        public static IServiceCollection AddBrandingServices4AzureStorage(this IServiceCollection services, string connectionString, string container, bool scoped)
        {
            if (scoped)
            {
                services.AddScoped<IBrandingStore>((serviceProvider) => new AzureStorageBrandingStore(connectionString, container));
            }
            else
            {
                services.AddTransient<IBrandingStore>((serviceProvider) => new AzureStorageBrandingStore(connectionString, container));
            }

            return services;
        }

        public static IServiceCollection AddBrandingServices4AzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddBrandingServices4AzureStorage(configuration, true);
        }
        
        public static IServiceCollection AddBrandingServices4AzureStorage(this IServiceCollection services, IConfiguration configuration, bool scoped)
        {
            // search the configuration
            var connectionString = configuration.GetValue<string>("Branding:Stores:AzureStorage:ConnectionString");
            var container = configuration.GetValue<string>("Branding:Stores:AzureStorage:Container");

            // setup
            return services.AddBrandingServices4AzureStorage(connectionString, container, scoped);
        }
    }
}

