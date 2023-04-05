using System;
using System.Threading.Tasks;

namespace CoreHelpers.Branding.Runtime
{
	public interface IBrandingManager
	{
        IBrandingBuilder CreateBuilder();

        Task<ICompanyBranding> FindCompanyBrandingByApplicationAndUniqueIdentifier(string applicationId, string brandingId, ICompanyBranding defaultValues);		
    }
}

