using System;
namespace CoreHelpers.Branding.Runtime.Services
{
	public class BrandingStateService : IBrandingStateService
    {
		public ICompanyBranding CurrentCompanyBranding { get; private set; }

        public void SetCurrentCompanyBranding(ICompanyBranding companyBranding)
        {
            this.CurrentCompanyBranding = companyBranding;
        }
    }
}

