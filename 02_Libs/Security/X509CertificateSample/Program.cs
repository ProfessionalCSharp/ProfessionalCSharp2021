using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Cryptography.X509Certificates;

using var host = Host
    .CreateDefaultBuilder(args)
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
    Console.WriteLine($"Key algorithm: {certificate.PublicKey.Key.KeyExchangeAlgorithm}");
    Console.WriteLine($"Key size: {certificate.PublicKey.Key.KeySize}");
}
