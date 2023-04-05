﻿using System;
using CoreHelpers.Branding.Runtime.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoreHelpers.Branding.Runtime
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBrandingServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandingManager, BrandingManager>();
            services.AddScoped<IBrandingStateService, BrandingStateService>();            
            return services;
        }
    }
}

