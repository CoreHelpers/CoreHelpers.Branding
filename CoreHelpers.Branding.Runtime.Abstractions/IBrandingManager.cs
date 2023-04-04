using System;
using System.Threading.Tasks;

namespace CoreHelpers.Branding.Runtime
{
	public interface IBrandingManager
	{
		Task<ICompanyBranding> FindCompanyBrandingByApplicationAndUniqueIdentifier(string applicationId, string brandingId, ICompanyBranding defaultValues);

		ICompanyBranding BuildMutableBranding(string name);
    }
}

