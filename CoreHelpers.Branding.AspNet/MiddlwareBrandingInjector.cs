using System;
using CoreHelpers.Branding.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace CoreHelpers.Branding.AspNet
{
    public static class MiddlwareBrandingInjector
    {        
        public static IApplicationBuilder UseBrandingWithRequestHost(this IApplicationBuilder app, string applicationId, Func<IBrandingManager, Task<ICompanyBranding>> defaultBrandingBuilder)
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
                    var defaultBranding = await defaultBrandingBuilder(brandingManager);
                    currentBranding = await brandingManager.FindCompanyBrandingByApplicationAndUniqueIdentifier(applicationId, ctx.Request.Host.Host.ToString().ToSha1(), defaultBranding);
                }

                // set the state
                brandingStateService.SetCurrentCompanyBranding(currentBranding);

                // next middleware 
                await next(ctx);
            });

            return app;
        }
    }
}

