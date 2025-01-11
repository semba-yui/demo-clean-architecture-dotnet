using System.ComponentModel.DataAnnotations;

namespace DemoCompany.DemoCleanArchitecture.Domain.Settings;

/// <summary>
///     アプリケーションの設定
/// </summary>
public sealed class DemoCleanArchitectureSettings
{
    /// <summary>
    ///     認証コードの有効期限（分）
    /// </summary>
    [Required]
    [Range(0, 1440)]
    public required int AuthCodeExpirationMinutes { get; init; }

    /// <summary>
    ///     JWT の署名キー
    /// </summary>
    [Required]
    public required string JwtSecret { get; init; }

    /// <summary>
    ///     JWT の発行者
    /// </summary>
    [Required]
    public required string JwtIssuer { get; init; }

    /// <summary>
    ///     JWT の受信者
    /// </summary>
    [Required]
    public required string JwtAudience { get; init; }

    /// <summary>
    ///     2段階認証までの一時的な JWT の有効期限（分）
    /// </summary>
    [Required]
    [Range(0, 1440)]
    public required int PartialJwtExpirationMinutes { get; init; }

    /// <summary>
    ///     2段階認証後の最終的な JWT の有効期限（分）
    /// </summary>
    [Required]
    [Range(0, 1440)]
    public required int FinalJwtExpirationMinutes { get; init; }

    /// <summary>
    ///     リフレッシュトークンの有効期限（日）
    /// </summary>
    [Required]
    [Range(0, 365)]
    public required int RefreshTokenExpirationDays { get; init; }

    /// <summary>
    ///     ログイン試行回数の最大値
    /// </summary>
    [Required]
    [Range(1, 100)]
    public required int MaxFailedAccessAttempts { get; init; }

    /// <summary>
    ///     ログインロックアウト時間（分）
    /// </summary>
    [Required]
    [Range(0, 1440)]
    public required int LoginLockoutMinutes { get; init; }
}
