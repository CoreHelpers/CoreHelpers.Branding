# Branding
Introducing branding in an application is very similar. This project offers an aproach to integrate the branding artifacts into an
application. The system relys on a generic store consumpt the abstract the persistency from the delivery. Currently the following
stores are supported:

* Azure Storage Account: This store has the branding manifest stored on an Azure Storage account

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

## Registering services
The system is compatible with the .NET Core Dependency Injection. The following requests allow to regsiter
the different services. This code should be added in the service configuration section of your application:

```
builder.Services.AddBrandingServices();
builder.Services.AddBrandingServices4AzureStorage(builder.Configuration);
```