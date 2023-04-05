using System;
using CoreHelpers.Branding.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace CoreHelpers.Branding.AspNet
{
    public static class MiddlwareBrandingInjector
    {        
        public static IApplicationBuilder UseBrandingWithRequestHost(this IApplicationBuilder app, string applicationId, Func<IBrandingBuilder, Task<ICompanyBranding>> defaultBrandingBuilder)
        {
            app.Use(async (ctx, next) =>
            {
                // get the branding manager
                var brandingManager = ctx.RequestServices.GetService<IBrandingManager>();
                var brandingStateService = ctx.RequestServices.GetService<IBrandingStateService>();
                if (brandingManager == null || brandingStateService == null)
                    throw new Exception("Failed to load the branding");
                
                // get the current brand
                var currentBranding = await brandingManager.FindCompanyBrandingByApplicationAndUniqueIdentifier(applicationId, ctx.Request.Host.Host.ToString().ToSha1(), null);
                if (currentBranding == null)
                {
                    var defaultBranding = await defaultBrandingBuilder(brandingManager.CreateBuilder());
                    currentBranding = await brandingManager.FindCompanyBrandingByApplicationAndUniqueIdentifier(applicationId, ctx.Request.Host.Host.ToString().ToSha1(), defaultBranding);
                }

                // set the state
                brandingStateService.SetCurrentCompanyBranding(currentBranding);

                // next middleware 
                await next(ctx);
            });

            return app;
        }

        public static IApplicationBuilder UseBrandingOfferingRestEndpoint(this IApplicationBuilder app, string endpoint)
        {
            app.Use(async (ctx, next) =>
            {
                if (!ctx.Request.Path.HasValue || !ctx.Request.Path.Value.ToLower().Equals(endpoint.ToLower()))
                {

                    // next middleware 
                    await next(ctx);

                    // done
                    return;
                }

                // get the current branding
                var brandingStateService = ctx.RequestServices.GetRequiredService<IBrandingStateService>();
                if (brandingStateService == null)
                    throw new Exception("BrandingStateService is not available");

                // check if we have a current state
                if (brandingStateService.CurrentCompanyBranding == null)
                    throw new Exception("No branding state is available");

                // write the branding as json
                await ctx.Response.WriteAsJsonAsync<ICompanyBranding>(brandingStateService.CurrentCompanyBranding);

                // done
                return;
            });

            return app;
        }
    }
}

