using System.Reflection.PortableExecutable;
using CoreHelpers.Branding.Runtime;
using Newtonsoft.Json;

namespace CoreHelpers.Branding.Stores.AzureStorage.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // read the data
        var dataAsString = File.ReadAllText("./DataFiles/BasicCompanyBranding.json");

        // deserialize
        var dataModel = JsonConvert.DeserializeObject<AzureStorageCompanyBranding>(dataAsString);

        // verify the properties
        Assert.Equal("https://acme.org/imprint", dataModel?.Legals[nLegalItems.imprint]);
        Assert.Equal("https://acme.org/privacy", dataModel?.Legals[nLegalItems.dataPrivacy]);
        Assert.Equal("https://acme.org/demo.png", dataModel?.Logos[nLogoSize.small]);

        Assert.Equal("#e20074", dataModel?.Colors.Primary);
        Assert.Equal("white", dataModel?.Colors.PrimaryFont);
    }
}
