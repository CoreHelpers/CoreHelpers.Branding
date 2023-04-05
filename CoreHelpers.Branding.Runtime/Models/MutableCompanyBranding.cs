using System;
using System.Collections.Generic;

namespace CoreHelpers.Branding.Runtime.Models
{
    public class MutableCompanyBrandingColors : ICompanyBrandingColors
    {
        public string Font { get; }

        public string FontHover { get; }

        public string FontActive { get; }

        public string Primary { get; }

        public string PrimaryHover { get; }
    }

    public class MutableCompanyBranding : ICompanyBranding
    {		
        public string Name { get; }

        public Dictionary<nLogoSize, string> Logos { get; } = new Dictionary<nLogoSize, string>();

        public Dictionary<nLegalItems, string> Legals { get; } = new Dictionary<nLegalItems, string>();

        public ICompanyBrandingColors Colors { get; } = new MutableCompanyBrandingColors();

        public MutableCompanyBranding(string name)
        {
            this.Name = name;
        }
    }
}

