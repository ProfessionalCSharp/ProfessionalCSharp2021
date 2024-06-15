using Microsoft.Extensions.Diagnostics.HealthChecks;

using WebSampleApp.Services;

namespace WebSampleApp;

public class CustomReadyCheck(HealthSample healthSample) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (healthSample.IsReady)
        {
            return Task.FromResult(HealthCheckResult.Healthy("healthy"));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("unhealthy"));
        }
    }
}
