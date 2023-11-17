using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<KeyVaultService>();
    }).Build();

using var scope = host.Services.CreateScope();

var service = scope.ServiceProvider.GetRequiredService<KeyVaultService>();
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
