using System.ComponentModel.DataAnnotations;

namespace DemoCompany.DemoCleanArchitecture.Domain.Settings;

/// <summary>
///     アプリケーションの設定
/// </summary>
public sealed class DemoCleanArchitectureSettings
{
    /// <summary>
    ///     認証コードの有効期限（秒）
    /// </summary>
    [Required]
    [Range(1, 3600)]
    public required int AuthCodeExpirationSeconds { get; init; }
}
