using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.Login;

/// <summary>
///     ログインレスポンス
/// </summary>
[DataContract]
public sealed class LoginResponse
{
    /// <summary>
    ///     アクセストークン
    /// </summary>
    public required string AccessToken { get; init; }

    /// <summary>
    ///     リフレッシュトークン
    /// </summary>
    public string? RefreshToken { get; init; }

    /// <summary>
    ///     2要素認証が有効かどうか
    /// </summary>
    public required bool IsTwoFactorEnabled { get; init; }
}
