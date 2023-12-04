using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<AliceRunner>();
        services.AddScoped<BobRunner>();
    })
    .Build();

using var scope1 = host.Services.CreateScope();

var alice = scope1.ServiceProvider.GetRequiredService<AliceRunner>();
var bob = scope1.ServiceProvider.GetRequiredService<BobRunner>();
var keyAlice = alice.GetPublicKey();
var aliceData = alice.GetDocumentAndSignature();
bob.VerifySignature(aliceData.Data, aliceData.Sign, keyAlice);

Console.ReadLine();
