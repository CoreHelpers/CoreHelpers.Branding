using System;
using System.Collections.Generic;
using CoreHelpers.Branding.Runtime;

namespace CoreHelpers.Branding.Stores.AzureStorage
{
	public class AzureStorageCompanyBranding : ICompanyBranding
    {		
        public string Name { get; set; }

        public Dictionary<nLogoSize, string> Logos { get; set; } = new Dictionary<nLogoSize, string>();

        public Dictionary<nLegalItems, string> Legals { get; set; } = new Dictionary<nLegalItems, string>();
    }
}

