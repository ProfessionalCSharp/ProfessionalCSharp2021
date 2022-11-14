using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(config =>
    {
        config.AddUserSecrets("e70aa3fa-11ce-4f6c-b5cd-e5ad67d85f4e");
    })
    .ConfigureServices(services =>
    {

    }).Build();

var config = host.Services.GetRequiredService<IConfiguration>();
// Run the examples asynchronously, wait for the results before proceeding

// add the connection string to user secrets
string storageConnectionString = config["StorageConnectionString"] ?? throw new InvalidOperationException("Missing StorageConnectionString");

BlobServiceClient serviceClient = new(storageConnectionString);
var containerClient = await CreateContainerAsync(serviceClient);
var containerClient = GetContainerClient("bastasample-20220222");
await UploadBlobsAsync(containerClient);
await ListContainerAsync(containerClient);
await DownloadBlobsAsync(containerClient);

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

async Task<BlobContainerClient> CreateContainerAsync(BlobServiceClient serviceClient)
{
    var response = await serviceClient.CreateBlobContainerAsync($"bastasample-{Guid.NewGuid()}",
        PublicAccessType.Blob);

    Console.WriteLine("A container has been created");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();
    return response.Value;
}

BlobContainerClient GetContainerClient(string container)
{
    return serviceClient.GetBlobContainerClient(container);
}

async Task UploadBlobsAsync(BlobContainerClient containerClient)
{
    try
    {
        for (int i = 0; i < 10000; i++)
        {
            SomeData data = new($"text {Random.Shared.Next(200)}", i);
            var binData = BinaryData.FromObjectAsJson(data);
            var response = await containerClient.UploadBlobAsync($"blob-3-{i}", binData);
        }
       
    }
    catch (Exception ex)
    {
        throw;
    }
}

async Task ListContainerAsync(BlobContainerClient containerClient)
{
    // List the blobs in the container.
    Console.WriteLine("List blobs in container.");

    var items = containerClient.GetBlobsAsync(BlobTraits.Metadata);
    await foreach (var item in items)
    {
        Console.WriteLine(item.Name);
    }

    Console.WriteLine("\r\nCompare the list in the console to the portal.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();
}

async Task DownloadBlobsAsync(BlobContainerClient blobContainerClient)
{
    await foreach (var item in blobContainerClient.GetBlobsAsync())
    {
        var blobClient = blobContainerClient.GetBlobClient(item.Name);
        var result = await blobClient.DownloadContentAsync();
        var data = result.Value.Content.ToObjectFromJson<SomeData>();
        
        Console.WriteLine(data);
    }
}

public record SomeData(string Text, int Number);
