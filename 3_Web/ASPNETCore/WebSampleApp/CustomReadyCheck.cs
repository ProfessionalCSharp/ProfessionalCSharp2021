using Microsoft.Extensions.Diagnostics.HealthChecks;

using WebSampleApp.Services;

namespace WebSampleApp;

public class CustomReadyCheck(HealthSample healthSample) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        => healthSample.IsReady ? Task.FromResult(HealthCheckResult.Healthy("healthy"))
                                : Task.FromResult(HealthCheckResult.Unhealthy("unhealthy"));
}
