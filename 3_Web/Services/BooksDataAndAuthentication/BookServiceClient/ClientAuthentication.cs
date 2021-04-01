using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;


public record ClientAuthenticationOptions
{
    public string? ClientId { get; init; }
    public string? TenantId { get; init; }
    public string? SignUpSignInPolicyId { get; init; }
}

public class ClientAuthentication
{
    private IPublicClientApplication _clientAuthApp;
    private string _clientId;
    private string _tenantId;
    private string _signinPolicyId;
    private ILogger _logger;
    public ClientAuthentication(IOptions<ClientAuthenticationOptions> options, ILogger<ClientAuthentication> logger)
    {
        _logger = logger;
        _clientId = options.Value.ClientId ?? throw new InvalidOperationException("Configure a client-id!");
        _tenantId = options.Value.TenantId ?? throw new InvalidOperationException("Configure a tenant-id!");
        _signinPolicyId = options.Value.SignUpSignInPolicyId ?? throw new InvalidOperationException("Configure SignUpSignInPolicyId");

        _clientAuthApp = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
            .WithRedirectUri("http://localhost")
            .Build();
    }

    public async Task LoginAsync()
    {
        try
        {
            string[] scopes = { "user.read" };
            string[] webApiScopes = { "https://procsharp.onmicrosoft.com/booksservices/Books.Read" };
            var accounts = await _clientAuthApp.GetAccountsAsync(_signinPolicyId);
            var firstAccount = accounts.FirstOrDefault();
            AuthenticationResult? result;
            if (firstAccount is not null)
            {
                result = await _clientAuthApp.AcquireTokenSilent(scopes, firstAccount)
                    .ExecuteAsync();
            }
            else
            {
                result = await _clientAuthApp.AcquireTokenInteractive(scopes)
                    .WithExtraScopesToConsent(webApiScopes)
                    .ExecuteAsync();
            }

            _logger.LogTrace("$logged in with {0}", result.Account.Username);
            _accessToken = result.AccessToken;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private string? _accessToken;

    public async ValueTask<string> GetAccesstokenAsync(bool refresh = false)
    {
        if (_accessToken is null || refresh)
        {
            await LoginAsync();
        }
        if (_accessToken is null)
        {
            throw new InvalidOperationException("No access token received!");
        }
        return _accessToken;
    }
}

