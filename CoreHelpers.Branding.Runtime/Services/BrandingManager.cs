using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreHelpers.Branding.Runtime.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CoreHelpers.Branding.Runtime.Services
{
	public class BrandingManager : IBrandingManager
    {
        private IEnumerable<IBrandingStore> _stores;
        private IMemoryCache _memoryCache;

        public BrandingManager(IEnumerable<IBrandingStore> stores, IMemoryCache memoryCache)
		{
            _stores = stores;
            _memoryCache = memoryCache;
        }

        public ICompanyBranding BuildMutableBranding(string name)
        {
            return new MutableCompanyBranding(name);
        }

        public async Task<ICompanyBranding> FindCompanyBrandingByApplicationAndUniqueIdentifier(string applicationId, string brandingId, ICompanyBranding defaultValues)
        {
            // lookup the cache
            var branding = _memoryCache.Get<ICompanyBranding>($"branding.{applicationId}.{brandingId}");
            if (branding != null)
                return branding;

            // search in the stores
            foreach (var store in _stores)
            {
                branding = await store.LoadBranding(applicationId, brandingId);
                if (branding != null)
                {
                    _memoryCache.Set<ICompanyBranding>($"branding.{applicationId}.{brandingId}", branding);
                    return branding;
                }                 
            }

            // at this point we reflect the default values
            _memoryCache.Set<ICompanyBranding>($"branding.{applicationId}.{brandingId}", defaultValues);
            return defaultValues;
            
        }
    }
}

