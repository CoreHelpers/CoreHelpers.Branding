using System;
using System.IO;
using System.Threading.Tasks;
using CoreHelpers.Branding.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
#if NETCOREAPP3_1
                await next.Invoke();
#else
                await next(ctx);
#endif
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
#if NETCOREAPP3_1
                    await next.Invoke();
#else
                    await next(ctx);
#endif


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

#if NETCOREAPP3_1
                // write the branding as json                
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(
                    brandingStateService.CurrentCompanyBranding,
                    new Newtonsoft.Json.JsonSerializerSettings {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                        Formatting = Newtonsoft.Json.Formatting.Indented
                        }
                    );

                await ctx.Response.WriteAsync(json);
#else
                // write the branding as json                                
                await ctx.Response.WriteAsJsonAsync<ICompanyBranding>(brandingStateService.CurrentCompanyBranding);
#endif

                
                // done
                return;
            });

            return app;
        }       
    }
}

