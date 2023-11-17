using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<AliceRunner>();
        services.AddScoped<BobRunner>();
    })
    .Build();

using var scope = host.Services.CreateScope();

var alice = scope.ServiceProvider.GetRequiredService<AliceRunner>();
var bob = scope.ServiceProvider.GetRequiredService<BobRunner>();
var keyAlice = alice.GetPublicKey();
var keyBob = bob.GetPublicKey();
(byte[] iv, byte[] encryptedData) = await alice.GetSecretMessageAsync(keyBob);
await bob.ReadMessageAsync(iv, encryptedData, keyAlice);
