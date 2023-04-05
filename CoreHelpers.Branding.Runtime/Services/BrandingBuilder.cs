using System;
using System.Collections.Generic;

namespace CoreHelpers.Branding.Runtime.Services
{
    public class BrandingBuilderColorModel : ICompanyBrandingColors
    {
        public string Font { get; set; } = string.Empty;

        public string FontHover { get; set; } = string.Empty;

        public string FontActive { get; set; } = string.Empty;

        public string Primary { get; set; } = string.Empty;

        public string PrimaryHover { get; set; } = string.Empty;
    }

    public class BrandingBuilderModel : ICompanyBranding
    {
        public string Name { get; set; } = string.Empty;

        public Dictionary<nLogoSize, string> Logos { get; } = new Dictionary<nLogoSize, string>();

        public Dictionary<nLegalItems, string> Legals { get; } = new Dictionary<nLegalItems, string>();

        public ICompanyBrandingColors Colors { get; set; } = new BrandingBuilderColorModel();
    }
  
    public class BrandingBuilder : IBrandingBuilder
    {
        private BrandingBuilderModel model = new BrandingBuilderModel();
        private BrandingBuilderColorModel colorModel = new BrandingBuilderColorModel();

        public BrandingBuilder()
        {
            model.Colors = colorModel;
        }

        public IBrandingBuilder SetName(string name)
        {
            model.Name = name;
            return this;
        }

        public IBrandingBuilder AddLogo(nLogoSize size, string logoUrl)
        {
            if (model.Logos.ContainsKey(size))
                model.Logos[size] = logoUrl;
            else
                model.Logos.Add(size, logoUrl);

            return this;
        }

        public IBrandingBuilder AddLegal(nLegalItems itemType, string itemUrl)
        {
            if (model.Legals.ContainsKey(itemType))
                model.Legals[itemType] = itemUrl;
            else
                model.Legals.Add(itemType, itemUrl);

            return this;
        }

        public IBrandingBuilder SetColor(nColorType colorType, string colorValue)
        {
            switch (colorType)
            {
                case nColorType.font:
                    colorModel.Font = colorValue;
                    break;
                case nColorType.fontActive:
                    colorModel.FontActive = colorValue;
                    break;
                case nColorType.fontHover:
                    colorModel.FontHover = colorValue;
                    break;
                case nColorType.primary:
                    colorModel.Primary = colorValue;
                    break;
                case nColorType.primaryHover:
                    colorModel.PrimaryHover = colorValue;
                    break;
                default:
                    throw new Exception("Unknonw color type");
            }
            return this;
        }

        public ICompanyBranding Build()
        {
            return model;
        }
    }
}

