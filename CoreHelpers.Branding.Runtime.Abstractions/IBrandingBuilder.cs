using System;
using System.Reflection;

namespace CoreHelpers.Branding.Runtime
{
    public enum nColorType
    {
        font,
        fontHover,
        fontActive,
        primary,
        primaryHover,
        primaryFont
    }

    public interface IBrandingBuilder
	{
        IBrandingBuilder SetName(string name);

        IBrandingBuilder AddLogo(nLogoSize size, string logoUrl);

        IBrandingBuilder AddLegal(nLegalItems itemType, string itemUrl);

        IBrandingBuilder SetColor(nColorType colorType, string colorValue);

        ICompanyBranding Build();        
    }
}

