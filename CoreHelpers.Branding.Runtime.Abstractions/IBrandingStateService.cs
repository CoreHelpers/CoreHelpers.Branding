using System;
namespace CoreHelpers.Branding.Runtime
{
	public interface IBrandingStateService
	{
		ICompanyBranding CurrentCompanyBranding { get; }

		void SetCurrentCompanyBranding(ICompanyBranding companyBranding);
	}
}

