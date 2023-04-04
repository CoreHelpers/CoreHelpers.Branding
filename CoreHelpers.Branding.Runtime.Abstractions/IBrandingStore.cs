using System;
using System.Threading.Tasks;

namespace CoreHelpers.Branding.Runtime
{
	public interface IBrandingStore
	{
        Task<ICompanyBranding> LoadBranding(string applicationId, string brandingId);
    }
}

