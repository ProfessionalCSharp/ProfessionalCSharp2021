using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Id = Microsoft.Identity.Client;

class Runner
{
    private readonly string _clientId;
    private readonly string _tenantId;
    private Id.IPublicClientApplication? _clientApp;
    private readonly ILogger _logger;

    public Runner(IConfiguration configuration, ILogger<Runner> logger)
    {
        _clientId = configuration["ClientId"] ?? throw new InvalidOperationException("Configure a ClientId");
        _tenantId = configuration["TenantId"] ?? throw new InvalidOperationException("Configure a TenantId");
        _logger = logger;
    }

    private void LogCallback(Id.LogLevel level, string message, bool containsPii)
    {
        LogLevel GetDotnetLogLevel(Id.LogLevel logLevel) =>
            logLevel switch
            {
                Id.LogLevel.Error => LogLevel.Error,
                Id.LogLevel.Warning => LogLevel.Warning,
                Id.LogLevel.Info => LogLevel.Information,
                Id.LogLevel.Verbose => LogLevel.Trace,
                _ => throw new InvalidOperationException("unexpected log level")
            };

        LogLevel logLevel = GetDotnetLogLevel(level);

        
        _logger.Log(logLevel, message);
    }

    public void Init()
    {
       _clientApp = Id.PublicClientApplicationBuilder
            .Create(_clientId)
            .WithLogging(LogCallback, logLevel: Id.LogLevel.Verbose, enableDefaultPlatformLogging: true)
            .WithAuthority(Id.AzureCloudInstance.AzurePublic, _tenantId)
            .WithRedirectUri("http://localhost")
            .Build();
    }

    public async Task LoginAsync()
    {
        if (_clientApp is null) throw new InvalidOperationException("Invoke Init first");

        try
        {
            string[] scopes = { "user.read" };
            var accounts = await _clientApp.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            if (firstAccount is not null)
            {
                Id.AuthenticationResult result = await _clientApp.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
                ShowAuthenticationResult(result);
            }
            else
            {
                Id.AuthenticationResult result = await _clientApp.AcquireTokenInteractive(scopes).ExecuteAsync();
                ShowAuthenticationResult(result);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    private void ShowAuthenticationResult(Id.AuthenticationResult result)
    {
        Console.WriteLine($"token: {result.AccessToken}");
        Console.WriteLine(result.Account.Username);
        Console.WriteLine(result.Account.Environment);
        Console.WriteLine(result.Account.HomeAccountId);
        foreach (var scope in result.Scopes)
        {
            Console.WriteLine(scope);
        }
    }
}

