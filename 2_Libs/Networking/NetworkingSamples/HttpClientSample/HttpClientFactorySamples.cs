using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class HttpClientFactorySamples
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    public HttpClientFactorySamples(
        IHttpClientFactory httpClientFactory,
        ILogger<HttpClientFactorySamples> logger)
    {
        _httpClient = httpClientFactory.CreateClient("cni");
        _logger = logger;
    }



}

