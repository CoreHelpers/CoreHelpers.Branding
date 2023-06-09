﻿using System;
using System.Collections.Generic;
using CoreHelpers.Branding.Runtime;
using Newtonsoft.Json;

namespace CoreHelpers.Branding.Stores.AzureStorage
{
    internal class AzureStorageCompanyBrandingColors : ICompanyBrandingColors
    {
        public string Font { get; set; }

        public string FontHover { get; set; }

        public string FontActive { get; set; }

        public string Primary { get; set; }

        public string PrimaryHover { get; set; }

        public string PrimaryFont { get; set; }
    }

    internal class AzureStorageCompanyBranding : ICompanyBranding
    {		
        public string Name { get; set; }

        public Dictionary<nLogoSize, string> Logos { get; set; } = new Dictionary<nLogoSize, string>();

        public Dictionary<nLegalItems, string> Legals { get; set; } = new Dictionary<nLegalItems, string>();

        [JsonConverter(typeof(ConcreteTypeConverter<AzureStorageCompanyBrandingColors>))]
        public ICompanyBrandingColors Colors { get; set; }

        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    }
}

