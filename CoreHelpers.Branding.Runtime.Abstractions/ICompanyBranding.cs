using System;
using System.Collections.Generic;

namespace CoreHelpers.Branding.Runtime
{
	public enum nLogoSize {
		small,
		medium,
		large
	}

	public enum nLegalItems
	{
		imprint,
		dataPrivacy
	}

	public interface ICompanyBranding
	{
		string Name { get; }

		Dictionary<nLogoSize, string> Logos { get; }

		Dictionary<nLegalItems, string> Legals { get; }
	}
}

