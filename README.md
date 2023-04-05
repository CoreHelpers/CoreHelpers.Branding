# Branding
Introducing branding in an application is very similar. This project offers an aproach to integrate the branding artifacts into an
application. The system relys on a generic store consumpt the abstract the persistency from the delivery. Currently the following
stores are supported:

* Azure Storage Account: This store has the branding manifest stored on an Azure Storage account

## Installation 
There are several NuGet packages installable

```
NuGet\Install-Package CoreHelpers.Branding.Runtime.Abstractions
NuGet\Install-Package CoreHelpers.Branding.Runtime
NuGet\Install-Package CoreHelpers.Branding.AspNet 
NuGet\Install-Package CoreHelpers.Branding.Stores.AzureStorage 
```

## Configuration
The system must be configured by adding the following section into the application configuration:

```json
"Branding": {   
}
```

### Configuration of the Azure Storage Store
The Azure Storage Store should be configured via the IConfiguration framework. When using configuration files
the structure is as follows:

```json
"Branding": {
    "Stores": {
        "AzureStorage": {
            "ConnectionString": "YOUR_CONNECTION_STRING",
            "Container": "YOUR_STORAGE_CONTAINER"
        }
    }              
}
```

## Register services
The system is compatible with the .NET Core Dependency Injection. The following requests allow to regsiter
the different services. This code should be added in the service configuration section of your application:

```
builder.Services.AddBrandingServices();
builder.Services.AddBrandingServices4AzureStorage(builder.Configuration);
```

## Apply branding based on request host
A common scenario is to apply the branding based on the host header of the incoming requets. The following middleware
allows injects the current branding based on the host header:

```
app.UseBrandingWithRequestHost("YOUR_APPLICATION_ID", async (builder) =>
{
    builder
        .SetName("YOUR_TITLE")
        .AddLogo(nLogoSize.small, "YOUR_LOGO_URI")
        .SetColor(nColorType.primary, "YOUR_COLOR")
        .SetColor(nColorType.primaryFont, "YOUR_COLOR");       

    await Task.CompletedTask;
    return builder.Build();
});
```

## Use current branding in views
The current branding in views can be used easily by injecting the brand state and using them like 
a view model. Add the following lines to _ViewImports.cshtml:

```
@inject CoreHelpers.Branding.Runtime.IBrandingStateService branding
```

In every view the branding model can be used as follows: 

```
<a class="link" target="_new" href="@branding.CurrentCompanyBranding.Legals[nLegalItems.imprint]">Imprint</a>
```
