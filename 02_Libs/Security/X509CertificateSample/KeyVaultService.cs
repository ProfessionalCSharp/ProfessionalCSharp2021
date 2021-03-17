using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


class KeyVaultService
{
    private readonly string _vaultUri;
    private readonly VisualStudioCredential _credential = new();
    public KeyVaultService(IConfiguration configuration)
    {
        _vaultUri = configuration["VaultUri"];
    }

    public async Task<X509Certificate2> GetCertificateAsync(string name)
    {
        //using var listener = new AzureEventSourceListener((eventArgs, text) => AzureEventSourceHandler(eventArgs, text), EventLevel.Informational);

        //static void AzureEventSourceHandler(EventWrittenEventArgs eventArgs, string text)
        //{
        //    Console.WriteLine(text);
        //    if (text.Contains("Credential") && text.Contains("succeeded"))
        //    {
        //        Console.WriteLine(text);
        //    }
        //    Console.WriteLine();
        //}

        CertificateClient certClient = new(new Uri(_vaultUri), _credential);
        Response<KeyVaultCertificateWithPolicy> response = await certClient.GetCertificateAsync("AliceCert");
        Uri secretId = response.Value.SecretId;
        string secretName = secretId.Segments[2].Trim('/');
        string version = secretId.Segments[3].TrimEnd('/');

        // https://stackoverflow.com/questions/37033073/how-can-i-create-an-x509certificate2-object-from-an-azure-key-vault-keybundle
        
        byte[] x509cer = response.Value.Cer;
        SecretClient secretClient = new(new Uri(_vaultUri), _credential);
        Response<KeyVaultSecret> responseSecret = await secretClient.GetSecretAsync(secretName, version);
        KeyVaultSecret secret = responseSecret.Value;
        byte[] privateKeyBytes = Convert.FromBase64String(secret.Value);
        X509Certificate2 cert = new(privateKeyBytes);
        return cert;
    }
}

