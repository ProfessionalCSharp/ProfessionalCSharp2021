using Microsoft.Extensions.Diagnostics.HealthChecks;

using WebSampleApp.Services;

namespace WebSampleApp;

public class CustomHealthCheck(HealthSample healthSample) : IHealthCheck
{
    private readonly HealthSample _healthSample = healthSample;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        => _healthSample.IsHealthy ? Task.FromResult(HealthCheckResult.Healthy("healthy"))
                                   : Task.FromResult(HealthCheckResult.Unhealthy("unhealthy"));
}
