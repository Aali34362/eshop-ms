using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.API.Utilities;

public class MapHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        bool healthCheckResultHealthy = true;

        if (healthCheckResultHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("The check indicates a healthy result."));
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("The check indicates an unhealthy result."));
    }
}

public class HealthCheckResponse
{
    public string? Status { get; set; }
    public IEnumerable<HealthCheck>? Checks { get; set; }
    public TimeSpan Duration { get; set; }
}

public class HealthCheck
{
    public string? Component { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
    public TimeSpan Duration { get; set; }
}
