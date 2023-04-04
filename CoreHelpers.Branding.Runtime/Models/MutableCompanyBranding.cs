using System;
using System.Collections.Generic;

namespace CoreHelpers.Branding.Runtime.Models
{
	public class MutableCompanyBranding : ICompanyBranding
    {		
        public string Name { get; }

        public Dictionary<nLogoSize, string> Logos { get; } = new Dictionary<nLogoSize, string>();

        public Dictionary<nLegalItems, string> Legals { get; } = new Dictionary<nLegalItems, string>();

        public MutableCompanyBranding(string name)
        {
            this.Name = name;
        }

    }
}

