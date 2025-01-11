using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.HealthChecks;

/// <summary>
///     起動時のヘルスチェック
/// </summary>
public sealed class StartupHealthCheck : IHealthCheck
{
    private bool _startupComplete;

    /// <summary>
    ///     アプリケーションの起動が完了したかどうかをチェックします。
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_startupComplete
            ? HealthCheckResult.Healthy("Startup tasks are completed.")
            : HealthCheckResult.Unhealthy("Startup is still in progress."));
    }

    /// <summary>
    ///     アプリケーションの起動が完了したことをマークします。
    /// </summary>
    public void MarkStartupComplete()
    {
        _startupComplete = true;
    }
}
