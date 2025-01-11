using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.HealthChecks;

/// <summary>
///     DB接続のヘルスチェック
/// </summary>
/// <param name="dbContext"></param>
public sealed class DbConnectivityHealthCheck(DemoCleanArchitectureDbContext dbContext) : IHealthCheck
{
    /// <summary>
    ///     DB接続のヘルスチェック
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            bool canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);

            return canConnect
                ? HealthCheckResult.Healthy("DB connection is OK")
                : HealthCheckResult.Unhealthy("Cannot connect to the DB");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"DB check failed: {ex.Message}", ex);
        }
    }
}
