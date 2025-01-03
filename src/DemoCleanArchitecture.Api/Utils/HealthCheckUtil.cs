using System.Net.Mime;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoCompany.DemoCleanArchitecture.Api.Utils;

/// <summary>
///     ヘルスチェックのユーティリティ
/// </summary>
public static class HealthCheckUtil
{
    /// <summary>
    ///     ヘルスチェックの結果をレスポンスに書き込みます。
    /// </summary>
    /// <param name="context"></param>
    /// <param name="report"></param>
    public static async Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry =>
                new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description,
                    duration = entry.Value.Duration.TotalMilliseconds,
                    exception = entry.Value.Exception?.Message,
                })
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
