using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

public class AuthenticationMessageHandler : DelegatingHandler
{
    private readonly ClientAuthentication _clientAuthentication;
    public AuthenticationMessageHandler(ClientAuthentication clientAuthentication)
    {
        _clientAuthentication = clientAuthentication;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string token = await _clientAuthentication.GetAccesstokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            token = await _clientAuthentication.GetAccesstokenAsync(refresh: true);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await base.SendAsync(request, cancellationToken);
        }
        return response;
    }
}

