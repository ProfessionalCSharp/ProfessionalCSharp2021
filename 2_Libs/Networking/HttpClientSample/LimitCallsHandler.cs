﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Net;

public record LimitCallsHandlerOptions
{
    public int LimitCalls { get; init; }
}

public class LimitCallsHandler : DelegatingHandler
{ 
    private readonly ILogger _logger;
    private readonly int _limitCount;
    private int _numberCalls = 0;
    public LimitCallsHandler(IOptions<LimitCallsHandlerOptions> options, ILogger<LimitCallsHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);

        _limitCount = options.Value.LimitCalls;
        _logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_numberCalls >= _limitCount)
        {
            _logger.LogInformation("limit reached, returning too many requests");
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.TooManyRequests));
        }
        Interlocked.Increment(ref _numberCalls);
        _logger.LogTrace("SendAsync from within LimitCallsHandler");
        return base.SendAsync(request, cancellationToken);
    }
}
