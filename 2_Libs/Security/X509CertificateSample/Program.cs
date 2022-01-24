using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddUserSecrets("080d7485-fc68-4aa9-a3f0-4bd93efef352");
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<KeyVaultService>();
    }).Build();

var service = host.Services.GetRequiredService<KeyVaultService>();
using var certificate = await service.GetCertificateAsync("AliceCert");

ShowCertificate(certificate);

void ShowCertificate(X509Certificate2 certificate)
{
    Console.WriteLine($"Subject: {certificate.Subject}");
    Console.WriteLine($"Not before: {certificate.NotBefore:D}");
    Console.WriteLine($"Not after: {certificate.NotAfter:D}");
    Console.WriteLine($"Has private key: {certificate.HasPrivateKey}");
    RSA? publicKey = certificate.PublicKey.GetRSAPublicKey();
    if (publicKey is not null)
    {
        Console.WriteLine($"Key algorithm: {publicKey.KeyExchangeAlgorithm}");
        Console.WriteLine($"Key size: {publicKey.KeySize}");
    }
}
