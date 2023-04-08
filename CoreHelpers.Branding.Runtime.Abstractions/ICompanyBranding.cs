using System;
using System.Collections.Generic;

namespace CoreHelpers.Branding.Runtime
{
	public enum nLogoSize {
		small,
		medium,
		large,
		fav16,
		fav32
	}

	public enum nLegalItems
	{
		imprint,
		dataPrivacy
	}

	public interface ICompanyBrandingColors
	{
        string Font { get; }
        string FontHover { get; }
        string FontActive { get; }
        string Primary { get; }
        string PrimaryHover { get; }
		string PrimaryFont { get; }
	}
    
    public interface ICompanyBranding
	{
		string Name { get; }

		Dictionary<nLogoSize, string> Logos { get; }

		Dictionary<nLegalItems, string> Legals { get; }

        ICompanyBrandingColors Colors { get; }
    }
}

