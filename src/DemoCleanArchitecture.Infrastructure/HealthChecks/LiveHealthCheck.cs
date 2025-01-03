using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.HealthChecks;

/// <summary>
///     アプリケーションのヘルスチェック
/// </summary>
public sealed class LiveHealthCheck : IHealthCheck
{
    /// <summary>
    ///     アプリケーションのヘルスチェック
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("Application is alive"));
    }
}
