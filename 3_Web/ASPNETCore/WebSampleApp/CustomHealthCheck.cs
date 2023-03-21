using Microsoft.Extensions.Diagnostics.HealthChecks;

using WebSampleApp.Services;

namespace WebSampleApp;

public class CustomHealthCheck : IHealthCheck
{
    private readonly HealthSample _healthSample;
    public CustomHealthCheck(HealthSample healthSample) => _healthSample = healthSample;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (_healthSample.IsHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("healthy"));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("unhealthy"));
        }
    }
}
