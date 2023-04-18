namespace CoreHelpers.Branding.Runtime.Tests;

using CoreHelpers.Branding.Runtime.Services;

public class BrandingBuilderTests
{
    [Fact]
    public void TestLogos()
    {
        var builder = new BrandingBuilder();
        Assert.Equal("small", builder.AddLogo(nLogoSize.small, "small").Build().Logos[nLogoSize.small]);
        Assert.Equal("medium", builder.AddLogo(nLogoSize.medium, "medium").Build().Logos[nLogoSize.medium]);
        Assert.Equal("large", builder.AddLogo(nLogoSize.large, "large").Build().Logos[nLogoSize.large]);
        Assert.Equal("fav16", builder.AddLogo(nLogoSize.fav16, "fav16").Build().Logos[nLogoSize.fav16]);
        Assert.Equal("fav32", builder.AddLogo(nLogoSize.fav32, "fav32").Build().Logos[nLogoSize.fav32]);
    }

    [Fact]
    public void TestLegals()
    {
        var builder = new BrandingBuilder();
        Assert.Equal("IMPRINT", builder.AddLegal(nLegalItems.imprint, "IMPRINT").Build().Legals[nLegalItems.imprint]);
        Assert.Equal("DP", builder.AddLegal(nLegalItems.dataPrivacy, "DP").Build().Legals[nLegalItems.dataPrivacy]);
    }

    [Fact]
    public void TestAttributes()
    {
        var builder = new BrandingBuilder();
        Assert.Equal("demo", builder.AddAttribute("attr01", "demo").Build().Attributes["attr01"]);        
    }
}
