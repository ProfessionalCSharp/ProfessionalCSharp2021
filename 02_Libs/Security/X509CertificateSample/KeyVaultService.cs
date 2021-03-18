using Azure;
using Azure.Core.Diagnostics;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

static class EventLevelExtensions
{
    public static LogLevel ToLogLevel(this EventLevel eventLevel)
        => eventLevel switch
        {
            EventLevel.Critical => LogLevel.Critical,
            EventLevel.Error => LogLevel.Error,
            EventLevel.Warning => LogLevel.Warning,
            EventLevel.Informational => LogLevel.Information,
            EventLevel.Verbose => LogLevel.Trace,
            EventLevel.LogAlways => LogLevel.Critical,
            _ => throw new InvalidOperationException("Update for a new event level")
        };
}

class KeyVaultService : IDisposable
{
    private readonly string _vaultUri;
    private readonly ILogger _logger;
    private readonly VisualStudioCredential _credential = new();
    private readonly AzureEventSourceListener _azureEventSourceListener;
    public KeyVaultService(IConfiguration configuration, ILogger<KeyVaultService> logger)
    {
        _vaultUri = configuration["VaultUri"];
        _logger = logger;
        _azureEventSourceListener = new AzureEventSourceListener((eventArgs, message) 
            => _logger.Log(eventArgs.Level.ToLogLevel(), message), EventLevel.Verbose);

    }

    public void Dispose()
        => _azureEventSourceListener.Dispose();

    public async Task<X509Certificate2> GetCertificateAsync(string name)
    {
        CertificateClientOptions options = new();
        options.Diagnostics.IsLoggingEnabled = true;
        options.Diagnostics.IsDistributedTracingEnabled = true;
        options.Diagnostics.IsLoggingContentEnabled = true;

        CertificateClient certClient = new(new Uri(_vaultUri), _credential, options);
        Response<KeyVaultCertificateWithPolicy> response = await certClient.GetCertificateAsync("AliceCert");
        Uri secretId = response.Value.SecretId;
        string secretName = secretId.Segments[2].Trim('/');
        string version = secretId.Segments[3].TrimEnd('/');
        
        byte[] x509cer = response.Value.Cer;
        SecretClient secretClient = new(new Uri(_vaultUri), _credential);
        Response<KeyVaultSecret> responseSecret = await secretClient.GetSecretAsync(secretName, version);
        KeyVaultSecret secret = responseSecret.Value;
        byte[] privateKeyBytes = Convert.FromBase64String(secret.Value);
        X509Certificate2 cert = new(privateKeyBytes);
        return cert;
    }
}
