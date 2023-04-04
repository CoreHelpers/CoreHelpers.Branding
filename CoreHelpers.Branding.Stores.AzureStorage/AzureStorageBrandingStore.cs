using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using CoreHelpers.Branding.Runtime;
using Newtonsoft.Json;

namespace CoreHelpers.Branding.Stores.AzureStorage
{
	public class AzureStorageBrandingStore : IBrandingStore
    {
        private string _connectionString;
        private string _containerName;

		public AzureStorageBrandingStore(string connectionString, string containerName)
		{
            _connectionString = connectionString;
            _containerName = containerName;
		}

        public async Task<ICompanyBranding> LoadBranding(string applicationId, string brandingId)
        {
            // get the refernce clients
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // build the manifest name
            var manifestBlobName = $"{brandingId}/{applicationId}/manifest.json";

            // get a blob client
            var blobItemClient = blobContainerClient.GetBlobClient(manifestBlobName);
            if (!await blobItemClient.ExistsAsync())
                return null;

            // load the manifest
            var blobStream = await blobItemClient.DownloadStreamingAsync();

            using (StreamReader reader = new StreamReader(blobStream.Value.Content))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    var ser = new Newtonsoft.Json.JsonSerializer();
                    return ser.Deserialize<AzureStorageCompanyBranding>(jsonReader);
                }
            }            
        }
    }
}

